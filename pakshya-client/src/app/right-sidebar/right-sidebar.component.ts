import { Component, OnInit } from '@angular/core';
import { PostService } from '../_services/post.service';
import { IPostStats, ICategoryStats } from '../_models/sidebarHelper';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-right-sidebar',
  templateUrl: './right-sidebar.component.html',
  styleUrls: ['./right-sidebar.component.scss']
})
export class RightSidebarComponent implements OnInit {

  constructor(private postService: PostService, private alertify: AlertifyService) { }
  postStats: IPostStats[];
  categoryStats: ICategoryStats[];
  ngOnInit() {
    this.postService.getPostStats().subscribe((postStats: IPostStats[]) => {
      this.postStats = postStats;
    }, err => {
      this.alertify.error('Some error occured');
    });
    this.postService.getCategoryStats().subscribe((categoryStats: ICategoryStats[]) => {
      this.categoryStats = categoryStats;
    }, err => {
      this.alertify.error('Some error occured');
    });
  }

}
