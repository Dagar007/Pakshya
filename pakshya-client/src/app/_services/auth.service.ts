import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IUser, IUserLoginFormValues, IUserRegisterFormValues } from '../_models/user';
import { Observable } from 'rxjs';

import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/user/';
  jwtHelper = new JwtHelperService(); 
  decodedToken: any;

  constructor(private http: HttpClient) { }

  login(loginFormValues : IUserLoginFormValues){
    console.log(loginFormValues);
    return this.http.post<IUser>(this.baseUrl+'login', loginFormValues).pipe(
      map((res: IUser) => {
        const user = res;
        if(user)
        {
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          localStorage.setItem('display', user.displayName)
          localStorage.setItem('username',user.username);
          
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
          localStorage.setItem('display', user.displayName);
          localStorage.setItem('username',user.username);
        }
      })
    )
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
