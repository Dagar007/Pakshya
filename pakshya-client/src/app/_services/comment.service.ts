import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private baseUrl = "http://localhost:5000/api/comment/";
  constructor(private http: HttpClient) { }

  deleteComment(id: string){
    return this.http.delete(this.baseUrl + id);
  }

  likeComment(id: string) {
    return this.http.post(this.baseUrl + id + "/like", {});
  }
  unlikeComment(id: string) {
    return this.http.delete(this.baseUrl + id + "/like");
  }

}
