import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, ParamMap, Params } from '@angular/router';
import { PostService } from 'src/app/post/post.service';
import { switchMap } from 'rxjs/operators';
import { v4 as uuid } from 'uuid';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IPostConcise, ICategory } from 'src/app/shared/_models/post';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class CreateComponent implements OnInit {
  categories: ICategory[];
  post: IPostConcise;
  id = '';
  page = 'Create';
  isImageEdited = 'n';

  createPostForm: FormGroup;

  public imagePath: string;
  imgURL: any;
  public message: string;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private postService: PostService,
    private alertify: AlertifyService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.postService.getCategories().subscribe((categories: ICategory[]) => {
      this.categories = categories;
    });
    this.createForm();
    this.loadPost();
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
      this.createPostForm.patchValue({ file: file });
      this.isImageEdited = 'y';
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event1) => {
        this.imgURL = reader.result;
      };
    }
  }
  removeImage() {
    this.imgURL = undefined;
    this.isImageEdited = 'y';
  }

  onSubmit() {
    if (this.createPostForm.valid) {
      const formData: any = new FormData();
      if (this.id) {
        formData.append('id', this.activatedRoute.snapshot.paramMap.get('id'));
        formData.append('heading', this.createPostForm.get('heading').value);
        formData.append(
          'description',
          this.createPostForm.get('description').value
        );
        formData.append(
          'categoryId',
          this.createPostForm.get('category').value.id
        );
        formData.append('isImageEdited', String(this.isImageEdited));
        formData.append('file', this.createPostForm.get('file').value);
        this.postService
          .updatePost(this.activatedRoute.snapshot.paramMap.get('id'), formData)
          .subscribe(
            () => {
              this.router.navigate(['/posts', this.post.id]);
            },
            (err) => {
              this.alertify.error(err);
            }
          );
      } else {
        formData.append('id', uuid());
        formData.append('heading', this.createPostForm.get('heading').value);
        formData.append(
          'description',
          this.createPostForm.get('description').value
        );
        formData.append(
          'categoryId',
          this.createPostForm.get('category').value.id
        );
        formData.append('file', this.createPostForm.get('file').value);
        this.postService.createPost(formData).subscribe(
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
    if (files.length === 0) {
      return;
    }

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
  private loadPost() {
    if (this.activatedRoute.snapshot.paramMap.get('id')) {
      this.postService
        .getPost(this.activatedRoute.snapshot.paramMap.get('id'))
        .subscribe(
          (post) => {
            this.post = post;
            this.createPostForm.patchValue(post);
            this.imgURL = post.photos[0].url;
            this.page = 'Edit';
          },
          (error) => {
            console.log(error);
          }
        );
    }
  }

  private createForm() {
    this.createPostForm = this.formBuilder.group({
      heading: ['', Validators.required],
      description: ['', Validators.required],
      category: ['', Validators.required],
      file: [''],
    });
  }
}
