import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPost } from '../_models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = 'http://localhost:5000/api/posts/';

  constructor(private http: HttpClient) { }

  getPosts() {
    return this.http.get<IPost[]>(this.baseUrl);
  }
  getPost(id: string) {
    return this.http.get<IPost>(this.baseUrl + id);
  }
  updatePost(post: IPost) {
    return this.http.put(this.baseUrl + post.id, post)
  }
  createPost(post: IPost) {
    return this.http.post(this.baseUrl, post);
  }
  deletePost(id: string) {
    return this.http.delete(this.baseUrl + id);
  }
  likePost(id: string) {
    return this.http.post(this.baseUrl + id +'/like',{});
  }
  unlikePost(id: string) {
    return this.http.delete(this.baseUrl + id +'/like');
  }
}
