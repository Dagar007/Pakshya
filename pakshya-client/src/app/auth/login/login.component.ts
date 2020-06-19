import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { IUserLoginFormValues } from 'src/app/_models/user';
// import { AuthService as FacebookAuth } from 'angularx-social-login';
// import { FacebookLoginProvider} from 'angularx-social-login';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService,
    // private facebookAuthService: FacebookAuth,
  ) {}

  loginFormValues: IUserLoginFormValues = {
    email: '',
    password: ''
  };

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

  // loginFacebook(platform: string): void {
  //   platform = FacebookLoginProvider.PROVIDER_ID;
  //      this.facebookAuthService.signIn(platform).then(
  //   (response) => {
  //       this.authService.fbLogin(response.authToken).subscribe((user) => {
  //         this.router.navigate(['/posts']);
  //       },
  //       (err: any) => {
  //         this.alertify.error(err);
  //         this.loginFormValues = {
  //           email: this.loginFormValues.email,
  //           password: ''
  //         };
  //       });
  //    });
  // }
}
