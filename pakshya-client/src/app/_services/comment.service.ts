import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private baseUrl = environment.apiUrl
  constructor(private http: HttpClient) { }

  deleteComment(id: string){
    return this.http.delete(this.baseUrl+'comment/' + id);
  }

  likeComment(id: string) {
    return this.http.post(this.baseUrl+'comment/' + id + "/like", {});
  }
  unlikeComment(id: string) {
    return this.http.delete(this.baseUrl+'comment/' + id + "/like");
  }

}
