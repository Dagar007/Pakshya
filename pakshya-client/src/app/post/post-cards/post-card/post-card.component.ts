import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IPost } from 'src/app/_models/post';
import { Router } from '@angular/router';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit {

  constructor(private router: Router, private postService: PostService) { }
  @Input() post: IPost;
  @Output() postDeleted = new EventEmitter<IPost>();
  ngOnInit() {
  }
  onPostDelete() {
    this.postService.deletePost(this.post.id).subscribe(() => {
      this.postDeleted.emit(this.post);
    })
  }

}
