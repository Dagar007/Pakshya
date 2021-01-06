import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { IProfile, IPhoto, IFollow } from '../shared/_models/profile';
import { BehaviorSubject, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private baseUrl = environment.apiUrl;

  private followingSource = new ReplaySubject<IFollow[]>(1);
  followings$ = this.followingSource.asObservable();

  private followersSource = new ReplaySubject<IFollow[]>(1);
  followers$ = this.followersSource.asObservable();

  private profilePhotoUrlSoruce = new BehaviorSubject<string>('../../assets/user.png');
  profilePhoto$ = this.profilePhotoUrlSoruce.asObservable();

  private profileIdSoruce = new ReplaySubject<string>(1);
  profileId$ = this.profileIdSoruce.asObservable();

  private canEditSource = new BehaviorSubject<boolean>(false);
  canEdit$ = this.canEditSource.asObservable();

  constructor(private httpService: HttpClient) { }

  setFollowers(followers: IFollow[]) {
    this.followersSource.next(followers);
  }
  setFollowings(followings: IFollow[]) {
    this.followingSource.next(followings);
  }

  changeProfilePhoto(url: string) {
    if (url) {
      this.profilePhotoUrlSoruce.next(url);
    } else {
      this.profilePhotoUrlSoruce.next('../../assets/user.png');
    }
  }

  changeId(id: string) {
    this.profileIdSoruce.next(id);
  }

  setCanEdit(canEdit: boolean) {
    this.canEditSource.next(canEdit);
  }

  get(username: string) {
    return this.httpService.get<IProfile>(this.baseUrl + 'profile/' + username);
  }

  follow(username: string) {
    return this.httpService.post(this.baseUrl + 'profile/' + username + '/follow', {});
  }
  unfollow(username: string) {
    return this.httpService.delete(this.baseUrl + 'profile/' + username + '/follow');
  }

  // photos related

  uploadPhoto(file: any) {
    return this.httpService.post<IPhoto>(this.baseUrl + 'photos', file);
  }
  setMainPhoto(id: string) {
    return this.httpService.post(this.baseUrl + 'photos/' + id + '/setmain', {});
  }
  deletePhoto(id: string) {
    return this.httpService.delete(this.baseUrl + 'photos/' + id);
  }
  updateBio(profile: IProfile) {
    return this.httpService.put(this.baseUrl + 'profile', profile);
  }

  getAllInterests() {
    return this.httpService.get(this.baseUrl + 'interest');
  }

  getUserInterests(id: string) {
    return this.httpService.get(this.baseUrl + 'interest/' + id + '/user');
  }

  updateUserInterests(interests: string[]) {
    return this.httpService.post(this.baseUrl + 'interest', {ids : interests});
  }
}
