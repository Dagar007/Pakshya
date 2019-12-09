import { Component, OnInit, Input } from '@angular/core';
import { IPost } from 'src/app/_models/post';

@Component({
  selector: 'app-post-card',
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit {

  constructor() { }
  @Input() post: IPost;
  ngOnInit() {
  }

}
