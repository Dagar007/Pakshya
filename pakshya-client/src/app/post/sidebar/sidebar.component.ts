import { Component, OnInit } from '@angular/core';
import { ICategory } from 'src/app/shared/_models/post';
import { PostService } from '../post.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  categories: ICategory[];
  constructor(private postService: PostService) { }

  ngOnInit() {
    this.postService.getCategories().subscribe((categories: ICategory[]) => {
      this.categories = categories;
    });
  }
  catgorySelected(id: string) {
    this.postService.catgorySelectedEmitter.emit(id);
    }
}
