import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
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

  ngOnInit() {
    this.isPostLiked();
    this.host = this.post.engagers.filter(x => x.isAuthor)[0];
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
  onLikeClick() {
    console.log("some");
  }

  isPostLiked() {
    if (this.authService.currentUser1)
      var currentUserName = this.authService.currentUser1.username;
    if (this.post.engagers.some(e => e.username == currentUserName)) {
      this.color = "primary";
    }
    if (
      this.post.engagers.some(e => e.username == currentUserName && e.isAuthor)
    ) {
      this.isAuthor1 = true;
    }
  }
}
