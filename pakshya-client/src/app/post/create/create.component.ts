import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IPost } from 'src/app/_models/post';
import { ActivatedRoute, Router, ParamMap, Params } from '@angular/router';
import { PostService } from 'src/app/_services/post.service';
import { switchMap } from 'rxjs/operators';
import { v4 as uuid } from "uuid";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {

  post$: Observable<IPost>;
  post: IPost = {
    id: null,
    heading: null,
    description: null,
    date: null,
    url: null,
    category: null,
    for: 0,
    against: 0
  }
  id: string = '';
  page: string = 'Create'
  constructor(private route: ActivatedRoute, private router: Router, private postService: PostService) { }

  ngOnInit() {
    //const id = this.route.snapshot.params['id'];
    //console.log(id);
    this.route.params.subscribe(
      (params: Params) => {
        this.id = params['id'];
      }
    )
    if (this.id) {
      this.post$ = this.route.paramMap.pipe(
        switchMap((params: ParamMap) => this.postService.getPost(params.get('id'))
        ));
      this.post$.subscribe((post: IPost) => {
        this.post = post;
        this.page = 'Edit'
      });
    } else {
      this.page = 'Create'
    }
  }
  onCancelCreateEditForm() {
    if (this.id) {
      console.log(this.id);
      this.router.navigate(['/posts', this.id])
    } else {
      this.router.navigate(['/posts'])
    }
  }
  onSubmit() {
    if(this.id) {
      this.post.date= new Date()
      console.log(this.post)
      this.postService.updatePost(this.post).subscribe(()=> {
        this.router.navigate(['/posts', this.post.id]);
      })
      
    }else {
      this.post.id = uuid();
      this.post.date= new Date()
      console.log(this.post)
      this.postService.createPost(this.post).subscribe(()=> {
        this.router.navigate(['/posts', this.post.id]);
      })
    }
  }
}
