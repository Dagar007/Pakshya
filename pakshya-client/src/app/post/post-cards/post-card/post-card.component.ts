import { Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import { PostService } from 'src/app/post/post.service';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { AuthService } from 'src/app/auth/auth.service';
import { IPostConcise, IEngagers } from 'src/app/shared/_models/post';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit {
  constructor(
    private postService: PostService,
    private alertify: AlertifyService
  ) {}

  @Input() post: IPostConcise;
  @Output() postDeleted = new EventEmitter<IPostConcise>();
  color: string;
  isAuthor1: boolean;
  host: IEngagers;
  noOfLikes: number;
  // currentUserName: string;

  like = false;

  ngOnInit() {
   if (this.post.isPostLiked) {
    this.color = 'primary';
   } else {
    this.color = '';
   }
  }

  onPostDelete() {
    this.postService.deletePost(this.post.id).subscribe(
      () => {
        this.postDeleted.emit(this.post);
      },
      err => {
        this.alertify.error(err);
      }
    );
  }

  likePost() {
    if (this.post.isPostLiked) {
      this.postService.unlikePost(this.post.id).subscribe(
        () => {
          this.post.isPostLiked = !this.post.isPostLiked;
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
          this.post.isPostLiked = !this.post.isPostLiked;
          this.color = 'primary';
          ++this.post.noOfLikes;
        },
        err => {
          this.alertify.error(err);
        }
      );
    }
  }
}
