import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/_services/post.service';
import { IPostConcise } from 'src/app/_models/post';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-post-cards',
  templateUrl: './post-cards.component.html',
  styleUrls: ['./post-cards.component.scss']
})
export class PostCardsComponent implements OnInit {
  pageNumber = 1;
  pageSize = 3;
  posts: IPostConcise[];
  isloading$: Observable<boolean>;
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
      this.setPage(1);
      this.posts = null;
      this.postService
        .getPosts(
          this.pagination.currentPage,
          this.pagination.itemsPerPage,
          this.userParams
        )
        .subscribe(
          (res: PaginatedResult<IPostConcise[]>) => {
            this.pagination = res.pagination;
            this.posts = res.result;
          },
          err => {
            this.alertify.error(err);
          }
        );
    });
  }

  getPosts() {
    this.postService
      .getPosts(
        this.pageNumber,
        this.pageSize,
        this.userParams.category != null ? this.userParams : null
      )
      .subscribe(
        (res: PaginatedResult<IPostConcise[]>) => {
          this.pagination = res.pagination;
          this.posts = res.result;
        },
        err => {
          this.alertify.error(err);
        }
      );
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

    this.postService
      .getPosts(
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.userParams.category != null ? this.userParams : null
      )
      .subscribe(
        (res: PaginatedResult<IPostConcise[]>) => {
          this.pagination = res.pagination;
          Array.prototype.push.apply(this.posts, res.result);
        },
        err => {
          this.alertify.error(err);
        }
      );
  }
  onScroll() {
    if (this.pagination.currentPage < this.pagination.totalPages) {
      this.loadNextPosts();
    }
  }
}
