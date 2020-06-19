import { Component, OnInit } from '@angular/core';
import { IUserLoginFormValues } from 'src/app/_models/user';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login-popup',
  templateUrl: './login-popup.component.html',
  styleUrls: ['./login-popup.component.scss']
})
export class LoginPopupComponent implements OnInit {

  loginFormValues: IUserLoginFormValues = {
    email: '',
    password: ''
  };

  constructor(private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit() {
    if (this.authService.loggedIn()) {
      this.router.navigate(['/']);
    }
  }

  onSubmit() {
    this.authService.login(this.loginFormValues).subscribe(
      () => {
        this.router.navigate(['/posts']);
      },
      err => {
        if (err === 'Unauthorized') {
          this.alertify.error('Incorrect username or password.');
        } else  {
          this.alertify.error(err);
        }
        this.loginFormValues = {
          email: this.loginFormValues.email,
          password: ''
        };
      }
    );
  }

}
