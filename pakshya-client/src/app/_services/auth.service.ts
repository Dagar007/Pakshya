import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs'
import { map } from 'rxjs/operators';
import { IUser, IUserLoginFormValues, IUserRegisterFormValues } from '../_models/user';


import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/user/';
  jwtHelper = new JwtHelperService(); 
  currentUser1: IUser;
  decodedToken: any;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  changeMainPhoto(photoUrl: string){
    this.photoUrl.next(photoUrl);
  }

  constructor(private http: HttpClient) { }

  login(loginFormValues : IUserLoginFormValues){
    return this.http.post<IUser>(this.baseUrl+'login', loginFormValues).pipe(
      map((res: IUser) => {
        const user = res;
        if(user)
        {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user))
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser1 = user;
          this.changeMainPhoto(this.currentUser1.image);
        }
      })
    );
  }

  currentUser() {
    return this.http.get<IUser>(this.baseUrl);
  }

  register(register :IUserRegisterFormValues) {
    return this.http.post<IUser>(this.baseUrl+'register',register).pipe(
      map((response: IUser) => {
        const user = response;
        if(user)
        {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user))
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser1 = user;
        }
      })
    )
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
