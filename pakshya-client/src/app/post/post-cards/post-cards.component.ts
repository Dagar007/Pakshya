import {
  Component,
  OnInit
} from "@angular/core";
import { PostService } from "src/app/_services/post.service";
import { IPostConcise } from "src/app/_models/post";
import { AlertifyService } from "src/app/_services/alertify.service";
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';

@Component({
  selector: "app-post-cards",
  templateUrl: "./post-cards.component.html",
  styleUrls: ["./post-cards.component.scss"]
})
export class PostCardsComponent implements OnInit {
  posts: IPostConcise[];
  loading = false;
  catgorySelected = "";
  userParams: any = {};
  pagination: Pagination;

  constructor(
    private postService: PostService,
    private alertify: AlertifyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getPosts();
    this.postService.catgorySelectedEmitter.subscribe(category => {
      this.userParams.category = category;
      this.setPage(0)
      this.loading = true;
      this.posts = null
      this.postService
        .getPosts(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
      .subscribe(
        (res: PaginatedResult<IPostConcise[]>) => {
          this.pagination = res.pagination;
          this.posts = res.result;
         
          this.loading = false;
        },
        err => {
          this.loading = false;
          this.alertify.error(err);
        })
      
      
    });
  }

  getPosts() {
    this.loading = true;
    this.route.data.subscribe(data => {
     this.pagination= data["posts"].pagination;
     
     this.posts = data["posts"].result;
     this.userParams.category = null;
     this.loading = false;
    }, err => {
      this.loading = false;
      this.alertify.error(err);
    })
  }
  postDeleted(deletedPost: IPostConcise) {
    this.posts = this.posts.filter(c => {
      return c.id !== deletedPost.id;
    });
  }
  
  setPage(page: number) {
    this.pagination.currentPage = page;
  }

  loadNextPosts() {
    this.setPage(this.pagination.currentPage + 1);
    this.loading = true;
    this.postService
      .getPosts(this.pagination.currentPage, this.pagination.itemsPerPage, this.userParams)
      .subscribe(
        (res: PaginatedResult<IPostConcise[]>) => {
          this.pagination = res.pagination;
          Array.prototype.push.apply(this.posts, res.result);
         
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
