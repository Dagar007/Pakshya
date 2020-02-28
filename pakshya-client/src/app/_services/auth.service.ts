import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs'
import { map } from 'rxjs/operators';
import { IUser, IUserLoginFormValues, IUserRegisterFormValues } from '../_models/user';
import * as fromRoot from './../app.reducer';
import { Store } from '@ngrx/store';
import * as UI from './../_shared/ui.actions';


import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService(); 
  currentUser1: IUser;
  decodedToken: any;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  changeMainPhoto(photoUrl: string){
    this.photoUrl.next(photoUrl);
  }

  constructor(private http: HttpClient, private store: Store<fromRoot.State>) { }

  login(loginFormValues : IUserLoginFormValues){
    this.store.dispatch(new UI.StartLoading());
    return this.http.post<IUser>(this.baseUrl+'user/'+'login', loginFormValues).pipe(
      map((res: IUser) => {
        const user = res;
        if(user)
        {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user))
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser1 = user;
          this.changeMainPhoto(this.currentUser1.image);
          this.store.dispatch(new UI.StopLoading());
        }
      })
    );
  }

  currentUser() {
    return this.http.get<IUser>(this.baseUrl+'user/');
  }

  register(register :IUserRegisterFormValues) {
    this.store.dispatch(new UI.StartLoading());
    return this.http.post<IUser>(this.baseUrl+'user/'+'register',register).pipe(
      map((response: IUser) => {
        const user = response;
        if(user)
        {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user))
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser1 = user;
          this.store.dispatch(new UI.StopLoading());
        }
      })
    )
  }

  fbLogin(accessToken: string) {
    this.store.dispatch(new UI.StartLoading());
    return this.http.post<IUser>(this.baseUrl +'user/facebook', {accessToken}).pipe(
      map((response: IUser) => {
        const user = response;
        if(user)
        {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user))
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser1 = user;
          this.store.dispatch(new UI.StopLoading());
        }
      })
    )
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
