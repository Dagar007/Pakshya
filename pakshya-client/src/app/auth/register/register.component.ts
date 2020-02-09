import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/_services/auth.service";
import { IUserRegisterFormValues, IUser } from "src/app/_models/user";
import { AlertifyService } from "src/app/_services/alertify.service";
import { Router } from "@angular/router";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from "@angular/forms";
import { CrossFieldErrorMatcher } from "src/app/_helper/CrossFieldErrorMatcher";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"]
})
export class RegisterComponent implements OnInit {
  maxDate;
  errorMatcher = new CrossFieldErrorMatcher();
  localBrithDay = new Date();
  register: IUserRegisterFormValues;

  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.createRegisterForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  createRegisterForm() {
    this.registerForm = this.fb.group(
      {
        gender: ["other"],
        username: ["", Validators.required],
        displayName: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
        birthday: [null, Validators.required],
        password: ["", [Validators.required, Validators.minLength(6)]],
        confirmPassword: ["", [Validators.required]],
        agree: ["", Validators.required]
      },
      {
        validators: this.passwordMatchValidator
      }
    );
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get("password").value === g.get("confirmPassword").value
      ? null
      : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      this.register = Object.assign({}, this.registerForm.value);
      this.authService.register(this.register).subscribe(
        () => {
          this.alertify.success("Logged In");
          this.router.navigate(["/posts"]);
        },
        err => {
          this.alertify.error(err);
        }
      );
    }
  }
}
