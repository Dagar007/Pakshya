import { Component, OnInit, Input } from '@angular/core';
import { IPost, IEngagers } from 'src/app/_models/post';

@Component({
  selector: 'app-post-delails-header',
  templateUrl: './post-delails-header.component.html',
  styleUrls: ['./post-delails-header.component.scss']
})
export class PostDelailsHeaderComponent implements OnInit {

  @Input() post: IPost;
  host: IEngagers;
  constructor() { }

  ngOnInit() {
    this.host = this.post.engagers.filter(x => x.isAuthor)[0];
  }

}
