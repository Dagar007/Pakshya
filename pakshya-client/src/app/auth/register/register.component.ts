import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { IUserRegisterFormValues, IUser } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  maxDate;
  localBrithDay = new Date();
  register: IUserRegisterFormValues = {
    username: '',
    email: '',
    password: '',
    birthday: '',
    displayName: '',
    gender: ''
  }
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }

  onSubmit(){

    let date = this.localBrithDay.getFullYear() + '-' + (this.localBrithDay.getMonth()+ 1)
    + '-' + (this.localBrithDay.getDate()<10? '0'+this.localBrithDay.getDate():this.localBrithDay.getDate())+ 'T00:00:00';
    this.register.birthday = date
    this.authService.register(this.register).subscribe((res: any)=>{
      this.alertify.success("Looged in");
      this.router.navigate(['/posts']);
    }, err => {
      this.alertify.error(err);
    })
  }
}
