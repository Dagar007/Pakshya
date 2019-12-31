import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { AuthService } from "src/app/_services/auth.service";
import { Router } from "@angular/router";
import { AlertifyService } from "src/app/_services/alertify.service";
import { IUserLoginFormValues } from "src/app/_models/user";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"]
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router,
    private alertify: AlertifyService
  ) {}

  loginFormValues: IUserLoginFormValues = {
    email: "",
    password: ""
  };

  ngOnInit() {}

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
}
