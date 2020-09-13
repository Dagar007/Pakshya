import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';
import { AlertifyService } from 'src/app/core/services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.scss']
})
export class ForgetPasswordComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router, private alertifyService: AlertifyService) { }
  isFormSubmitted = false;
  forgetFormValues = {
    email: '',
  };
  ngOnInit() {
    if (this.authService.loggedIn()) {
      this.router.navigate(['/']);
    }
  }

  onSubmit() {
    this.authService.forgetPassword(this.forgetFormValues.email).subscribe(() => {
      this.isFormSubmitted = true;
    }, err => {
      this.alertifyService.error(err);
    });
  }

}
