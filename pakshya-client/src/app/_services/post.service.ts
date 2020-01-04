import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IPost, IPostsEnvelope, ICategory } from "../_models/post";
import { IPostStats, ICategoryStats } from '../_models/sidebarHelper';


@Injectable({
  providedIn: "root"
})
export class PostService {
  private baseUrl = "http://localhost:5000/api/posts/";


  constructor(private http: HttpClient) {}

  getPosts(limit?:number, page?:number) {
    var offset = page? page * limit!: 0
    return this.http.get<IPostsEnvelope>(this.baseUrl+'?limit='+limit+'&offset='+offset);
  }
  getPost(id: string) {
    return this.http.get<IPost>(this.baseUrl + id);
  }
  updatePost(post: IPost) {
    return this.http.put(this.baseUrl + post.id, post);
  }
  createPost(post: IPost) {
    return this.http.post(this.baseUrl, post);
  }
  deletePost(id: string) {
    return this.http.delete(this.baseUrl + id);
  }
  likePost(id: string) {
    return this.http.post(this.baseUrl + id + "/like", {});
  }
  unlikePost(id: string) {
    return this.http.delete(this.baseUrl + id + "/like");
  }
  getCategories(){
    return this.http.get<ICategory[]>(this.baseUrl + 'category');
  }
  getPostStats() {
    return this.http.get<IPostStats[]>(this.baseUrl+'stats');
  }
  getCategoryStats() {
    return this.http.get<ICategoryStats[]>(this.baseUrl+'category/stats');
  }
}
