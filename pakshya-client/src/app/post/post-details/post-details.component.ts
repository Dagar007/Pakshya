import { Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { PostService } from 'src/app/post/post.service';
import { switchMap } from 'rxjs/operators';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { IPostConcise } from 'src/app/shared/_models/post';


@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.scss']
})
export class PostDetailsComponent implements OnInit {


  post$: Observable<IPostConcise>;
  post: IPostConcise;
  constructor(
    private route: ActivatedRoute,
    private postService: PostService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.post$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.postService.getPost(params.get('id'))
      )
    );
    this.post$.subscribe(
      (post: IPostConcise) => {
        this.post = post;
      },
      err => {
        this.alertify.error(err);
      }
    );
  }

  commentsModified(modification: string) {
    if (modification === 'added') {
      this.post.noOfComments++;
    } else if (modification === 'deleted') {
      this.post.noOfComments--;
    }
  }


}
