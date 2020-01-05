import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProfile, IPhoto } from '../_models/profile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  baseUrl = 'http://localhost:5000/api/profile/';
  baseUrlPhotos = 'http://localhost:5000/api/photos/';
  constructor(private httpService: HttpClient) { }

  get(username: string) {
    return this.httpService.get<IProfile>(this.baseUrl+ username);
  }

  follow(username: string) {
    return this.httpService.post(this.baseUrl+ username + '/follow',{});
  }
  unfollow(username: string) {
    return this.httpService.delete(this.baseUrl+ username + '/follow');
  }

  //photos related

  uploadPhoto(file: any) {
    return this.httpService.post<IPhoto>(this.baseUrlPhotos, file);
  }
  setMainPhoto(id: string) {
    return this.httpService.post(this.baseUrlPhotos + id + '/setmain',{});
  }
  deletePhoto(id: string) {
    return this.httpService.delete(this.baseUrlPhotos + id);
  }

  // Bio related

  updateBio(profile: IProfile) {
    return this.httpService.put(this.baseUrl, profile);
  }

}
