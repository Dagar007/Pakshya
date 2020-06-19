import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { IUserRegisterFormValues } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder
} from '@angular/forms';
import { CrossFieldErrorMatcher } from 'src/app/_helper/CrossFieldErrorMatcher';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
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
    if (this.authService.loggedIn()) {
      this.router.navigate(['/']);
    }
    this.createRegisterForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  createRegisterForm() {
    this.registerForm = this.fb.group(
      {
        gender: ['other',  Validators.required],
        username: ['', Validators.required],
        displayName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        birthday: [null, Validators.required],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validators: this.passwordMatchValidator
      }
    );
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value
      ? null
      : { mismatch: true };
  }

  onSubmit() {
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      this.register = Object.assign({}, this.registerForm.value);
      this.authService.register(this.register).subscribe(
        () => {
          this.registrationFormReset();
          this.alertify.success('Email confirmation link as been sent to your email.');
        },
        err => {
          this.alertify.error(err);
        }
      );
    }
  }

  private registrationFormReset() {
    this.registerForm.reset({'agree' : false, 'gender' : 'other'});
    Object.keys(this.registerForm.controls).forEach(key => {
      this.registerForm.get(key).setErrors(null);
    });
  }
}
