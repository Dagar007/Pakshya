import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPost } from '../_models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = 'http://localhost:5000/api/posts';

  constructor(private http: HttpClient) { }

  getPosts() {
    return this.http.get<IPost[]>(this.baseUrl);
  }
}
