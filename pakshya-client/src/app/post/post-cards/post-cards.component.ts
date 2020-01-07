import {
  Component,
  OnInit
} from "@angular/core";
import { PostService } from "src/app/_services/post.service";
import { IPost, IPostsEnvelope, IPostConcise } from "src/app/_models/post";
import { AlertifyService } from "src/app/_services/alertify.service";

@Component({
  selector: "app-post-cards",
  templateUrl: "./post-cards.component.html",
  styleUrls: ["./post-cards.component.scss"]
})
export class PostCardsComponent implements OnInit {
  posts: IPostConcise[];
  LIMIT = 2;
  postCount = 0;
  page = 0;
  totalPages = 0;
  loading = false;
  catgorySelected = "";

  constructor(
    private postService: PostService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.getPosts();
    this.postService.catgorySelectedEmitter.subscribe(category => {
      this.catgorySelected = category;
      this.setPage(0)
      this.loading = true;
      this.posts = null
      this.postService
        .getPosts(this.LIMIT, this.page, "", this.catgorySelected)
        .subscribe(
          (postsEnvelope: IPostsEnvelope) => {
            console.log(this.catgorySelected);
            this.posts = postsEnvelope.posts;
            this.postCount = postsEnvelope.postCount;
            this.loading = false;
          },
          err => {
            this.loading = false;
            this.alertify.error(err);
          }
        );
    });
  }

  getPosts() {
    this.loading = true;
    this.postService
      .getPosts(this.LIMIT, this.page, "", this.catgorySelected)
      .subscribe(
        (postsEnvelope: IPostsEnvelope) => {
          this.posts = postsEnvelope.posts;
          this.postCount = postsEnvelope.postCount;
          this.loading = false;
        },
        err => {
          this.loading = false;
          this.alertify.error(err);
        }
      );
  }
  postDeleted(deletedPost: IPostConcise) {
    this.posts = this.posts.filter(c => {
      return c.id !== deletedPost.id;
    });
  }
  getTotalPages() {
    return Math.ceil(this.postCount / this.LIMIT);
  }
  setPage(page: number) {
    this.page = page;
  }

  loadNextPosts() {
    this.setPage(this.page + 1);
    this.loading = true;
    this.postService
      .getPosts(this.LIMIT, this.page, "", this.catgorySelected)
      .subscribe(
        (postsEnvelope: IPostsEnvelope) => {
          Array.prototype.push.apply(this.posts, postsEnvelope.posts);
          this.postCount = postsEnvelope.postCount;
          this.loading = false;
        },
        err => {
          this.loading = false;
          this.alertify.error(err);
        }
      );
  }
  onScroll() {
    this.loadNextPosts();
  }
}
