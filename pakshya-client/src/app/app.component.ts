import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { IUser } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  ngOnInit() {
    const user: IUser =JSON.parse(localStorage.getItem('user'));
    if(user){
      this.authService.currentUser1 = user;
      this.authService.changeMainPhoto(user.image);
    }
  }
  constructor( private authService: AuthService) {}
}
