import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth/auth.service';
import { IUser } from './shared/_models/user';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  ngOnInit() {
    this.loadCurrentUser();
  }
  constructor( private authService: AuthService) {}

  loadCurrentUser() {
    this.authService.loadCurrentUser().subscribe(() => {
        console.log('loaded user');
      }, err => {
        console.log(err);
      });
  }
}
