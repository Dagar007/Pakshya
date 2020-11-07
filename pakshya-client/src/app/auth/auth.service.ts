import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { IUser, IUserLoginFormValues, IUserRegisterFormValues } from '../shared/_models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();

  private loggedInUserSource = new ReplaySubject<IUser>(1);
  loggedInUser$ = this.loggedInUserSource.asObservable();

  decodedToken: any;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  changeMainPhoto(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  changeCurrentUser(loggedInUser: IUser) {
    this.loggedInUserSource.next(loggedInUser);
  }

  loadCurrentUser() {
    if (localStorage.getItem('token') == null) {
      this.loggedInUserSource.next(null);
      return of(null);
    }
    return this.http.get(this.baseUrl + 'user').pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.loggedInUserSource.next(user);
        }
      })
    );
  }

  constructor(private http: HttpClient, private router: Router) { }

  login(loginFormValues: IUserLoginFormValues) {
    return this.http.post<IUser>(this.baseUrl + 'user/' + 'login', loginFormValues).pipe(
      map((res: IUser) => {
        const user = res;
        if (user) {
          localStorage.setItem('token', user.token);
          this.loggedInUserSource.next(user);
        }
      })
    );
  }

  currentUser() {
    return this.http.get<IUser>(this.baseUrl + 'user/');
  }

  register(register: IUserRegisterFormValues) {
    return this.http.post(this.baseUrl + 'user/' + 'register', register);
  }

  fbLogin(accessToken: string) {
    return this.http.post<IUser>(this.baseUrl + 'user/facebook', {accessToken}).pipe(
      map((response: IUser) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.loggedInUserSource.next(user);
        }
      })
    );
  }

  forgetPassword(email: string) {
    return this.http.post(this.baseUrl + 'user/forget', { email });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token');
    this.loggedInUserSource.next(null);
    this.router.navigate(['/login']);
  }
}
