import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProfile, IPhoto } from '../_models/profile';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private baseUrl = environment.apiUrl;
  constructor(private httpService: HttpClient) { }

  get(username: string) {
    return this.httpService.get<IProfile>(this.baseUrl+'profile/' + username);
  }

  follow(username: string) {
    return this.httpService.post(this.baseUrl+'profile/'+ username + '/follow',{});
  }
  unfollow(username: string) {
    return this.httpService.delete(this.baseUrl+'profile/'+ username + '/follow');
  }

  //photos related

  uploadPhoto(file: any) {
    return this.httpService.post<IPhoto>(this.baseUrl+'photos', file);
  }
  setMainPhoto(id: string) {
    return this.httpService.post(this.baseUrl+'photos/' + id + '/setmain',{});
  }
  deletePhoto(id: string) {
    return this.httpService.delete(this.baseUrl+'photos/' + id);
  }

  // Bio related

  updateBio(profile: IProfile) {
    return this.httpService.put(this.baseUrl+'profile', profile);
  }

}
