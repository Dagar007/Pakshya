import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { IUser } from 'src/app/shared/_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  loggedInUser$: Observable<IUser>;
  constructor(private router: Router, public authService: AuthService) {}
  ngOnInit() {
    this.loggedInUser$ = this.authService.loggedInUser$;
  }


  logout() {
   this.authService.logout();
  }
}
