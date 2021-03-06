import { Component, OnInit, Input } from '@angular/core';
import { PostService } from 'src/app/post/post.service';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AuthService } from 'src/app/auth/auth.service';
import { IPostConcise, IEngagers } from 'src/app/shared/_models/post';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/_models/user';

@Component({
  selector: 'app-post-delails-header',
  templateUrl: './post-delails-header.component.html',
  styleUrls: ['./post-delails-header.component.scss']
})
export class PostDelailsHeaderComponent implements OnInit {

  @Input() post: IPostConcise;
  host: IEngagers;
  color: string;
  isAuthor1: boolean;
  // currentUserName: string;
  loggedInUser$: Observable<IUser>;

  like = false;
  constructor(private postService: PostService,
    private alertify: AlertifyService,
    private authService: AuthService) { }

  ngOnInit() {
    this.loggedInUser$ = this.authService.loggedInUser$;
    this.isPostLiked();
  }

  likePost() {
    if (this.like) {
      this.postService.unlikePost(this.post.id).subscribe(
        () => {
          this.like = !this.like;
          this.color = '';
          --this.post.noOfLikes;
        },
        err => {
          this.alertify.error(err);
        }
      );
    } else {
      this.postService.likePost(this.post.id).subscribe(
        () => {
          this.like = !this.like;
          this.color = 'primary';
          ++this.post.noOfLikes;
        },
        err => {
          this.alertify.error(err);
        }
      );
    }
  }

  isPostLiked() {
    // if (this.authService.currentUser1) {
    //   this.currentUserName = this.authService.currentUser1.username;
    // }
    if (this.post.isPostLiked) {
      this.color = 'primary';
      this.like = true;
    }
    if (this.post.isAuthor) {
      this.isAuthor1 = true;
    }
  }

}
