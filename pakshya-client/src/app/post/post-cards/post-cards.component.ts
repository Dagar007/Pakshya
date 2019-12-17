import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/_services/post.service';
import { IPost } from 'src/app/_models/post';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-post-cards',
  templateUrl: './post-cards.component.html',
  styleUrls: ['./post-cards.component.scss']
})
export class PostCardsComponent implements OnInit {
  posts: IPost[];
  constructor(private postService: PostService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getPosts();
  }

  getPosts() {
    this.postService.getPosts().subscribe((posts: IPost[]) => {
      this.posts = posts;
    }, err => {
      this.alertify.error(err);
    });
  }
  postDeleted(deletedPost: IPost) {
    this.posts = this.posts.filter((c) => {
      return c.id !== deletedPost.id;
    })
  }
}
