import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, OnChanges } from "@angular/core";
import { IPost, IEngagers } from "src/app/_models/post";
import { PostService } from "src/app/_services/post.service";
import { AlertifyService } from "src/app/_services/alertify.service";
import { AuthService } from "src/app/_services/auth.service";

@Component({
  selector: "app-post-card",
  templateUrl: "./post-card.component.html",
  styleUrls: ["./post-card.component.scss"]
})
export class PostCardComponent implements OnInit {
  constructor(
    private postService: PostService,
    private alertify: AlertifyService,
    private authService: AuthService
  ) {}

  @Input() post: IPost;
  @Output() postDeleted = new EventEmitter<IPost>();
  color: string;
  isAuthor1: boolean;
  host: IEngagers;
  noOfLikes: number;
  currentUserName: string;

  like: boolean = false;

  ngOnInit() {
    this.host = this.post.engagers.filter(x => x.isAuthor)[0];
    this.noOfLikes = this.post.engagers.length;
    this.isPostLiked();
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
  isPostLiked() {
    if (this.authService.currentUser1)
      this.currentUserName = this.authService.currentUser1.username;
    if (this.post.engagers.some(e => e.username == this.currentUserName)) {
      this.color = "primary";
      this.like = true;
    }
    if (
      this.post.engagers.some(
        e => e.username == this.currentUserName && e.isAuthor
      )
    ) {
      this.isAuthor1 = true;
    }
  }

  likePost() {
    if (this.like) {
      this.postService.unlikePost(this.post.id).subscribe(
        () => {
          this.like = !this.like;
          this.color = "";
          --this.noOfLikes;
        },
        err => {
          this.alertify.error(err);
        }
      );
    } else {
      this.postService.likePost(this.post.id).subscribe(
        () => {
          this.like = !this.like;
          this.color = "primary";
          ++this.noOfLikes;
        },
        err => {
          this.alertify.error(err);
        }
      );
    }
  }
}
