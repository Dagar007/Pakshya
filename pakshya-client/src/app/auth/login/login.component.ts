import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/_services/auth.service";
import { Router } from "@angular/router";
import { AlertifyService } from "src/app/_services/alertify.service";
import { IUserLoginFormValues } from "src/app/_models/user";
import { AuthService as FacebookAuth } from 'angularx-social-login';
import { FacebookLoginProvider} from 'angularx-social-login';
import * as fromRoot from './../../app.reducer';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {

  isLoading$: Observable<boolean>
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService,
    private facebookAuthService: FacebookAuth,
    private store: Store<fromRoot.State>
  ) {}

  loginFormValues: IUserLoginFormValues = {
    email: "",
    password: ""
  };

  ngOnInit() {
    this.isLoading$ = this.store.select(fromRoot.getIsLoading);
  }

  onSubmit() {
    this.authService.login(this.loginFormValues).subscribe(
      () => {
        this.router.navigate(["/posts"]); 
      },
      err => {
        this.alertify.error(err);
        this.loginFormValues = {
          email: this.loginFormValues.email,
          password: ""
        };
      }
    );
  }

  loginFacebook(platform: string): void {
    platform = FacebookLoginProvider.PROVIDER_ID;
       this.facebookAuthService.signIn(platform).then(
    (response) => {
        this.authService.fbLogin(response.authToken).subscribe((user)=> {
          this.router.navigate(["/posts"]);
        })    
     }),
     (err: any) => {
      this.alertify.error(err);
      this.loginFormValues = {
        email: this.loginFormValues.email,
        password: ""
      };
    }
  }
}
