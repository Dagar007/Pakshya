import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICategory, IPostConcise } from 'src/app/_models/post';
import { ActivatedRoute, Router, ParamMap, Params } from '@angular/router';
import { PostService } from 'src/app/_services/post.service';
import { switchMap } from 'rxjs/operators';
import { v4 as uuid } from 'uuid';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent implements OnInit {
  post$: Observable<IPostConcise>;
  categories: ICategory[];
  post: IPostConcise;
  id = '';
  page = 'Create';
  formData: FormData;
  isImageEdited = false;

  createPostForm: FormGroup;

  public imagePath;
  imgURL: any;
  public message: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private postService: PostService,
    private alertify: AlertifyService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.postService.getCategories().subscribe((categories: ICategory[]) => {
      this.categories = categories;
    });

    this.createPostForm = this.formBuilder.group({
      heading: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      file: [''],
    });

    this.route.params.subscribe((params: Params) => {
      this.id = params['id'];
    });
    if (this.id) {
      this.post$ = this.route.paramMap.pipe(
        switchMap((params: ParamMap) =>
          this.postService.getPost(params.get('id'))
        )
      );
      this.post$.subscribe(
        (post: IPostConcise) => {
          this.createPostForm = this.formBuilder.group({
            heading: post.heading,
            description: post.description,
            category: post.category,
          });
          this.imgURL = post.photos[0].url;
          this.post = post;
          this.page = 'Edit';
        },
        (err) => {
          this.alertify.error(err);
        }
      );
    } else {
      this.page = 'Create';
    }
  }
  onCancelCreateEditForm() {
    if (this.id) {
      this.router.navigate(['/posts', this.id]);
    } else {
      this.router.navigate(['/posts']);
    }
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.formData = new FormData();
      this.formData.append('file', file);
      this.isImageEdited = true;
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = event1 => {
        this.imgURL = reader.result;
      };
      // this.createPostForm.get("file").setValue(file);
    }
  }
  removeImage() {
    if (this.formData) {
      this.formData.delete('file');
    }
    this.imgURL = undefined;
  }

  onSubmit() {
    if (this.createPostForm.valid) {
      if (!this.formData) {
        this.formData = new FormData();
      }
      if (this.id) {
        this.post = Object.assign({}, this.createPostForm.value);
        this.post.id = this.id;
        this.post.date = new Date();
        this.post.isImageEdited = this.isImageEdited;
        this.formData.append('jsonPost', JSON.stringify(this.post));
        this.postService.updatePost(this.id, this.formData).subscribe(
          () => {
            this.router.navigate(['/posts', this.post.id]);
          },
          (err) => {
            this.alertify.error(err);
          }
        );
      } else {
        this.post = Object.assign({}, this.createPostForm.value);
        this.post.id = uuid();
        this.post.date = new Date();
        this.formData.append('jsonPost', JSON.stringify(this.post));

        this.postService.createPost(this.formData).subscribe(
          () => {
            this.router.navigate(['/posts', this.post.id]);
          },
          (err) => {
            this.alertify.error(err);
          }
        );
      }
    }
  }

  preview(files) {
    if (files.length === 0) { return; }

    const mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = 'Only images are supported.';
      return;
    }

    const reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.imgURL = reader.result;
    };
  }
  compareObjects(o1: any, o2: any): boolean {
    return o1.name === o2.name && o1.id === o2.id;
  }
}
