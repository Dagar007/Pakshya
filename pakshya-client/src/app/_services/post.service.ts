import { Injectable, EventEmitter } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { IPost, IPostsEnvelope, ICategory } from "../_models/post";
import { IPostStats, ICategoryStats } from '../_models/sidebarHelper';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: "root"
})
export class PostService {
  private baseUrl = environment.apiUrl;

  catgorySelectedEmitter = new EventEmitter<string>();
  constructor(private http: HttpClient) {}

  getPosts(limit?:number, page?:number, username?: string, category?:string) {
    var offset = page? page * limit!: 0
    return this.http.get<IPostsEnvelope>(this.baseUrl+'posts/'+'?limit='+limit+'&offset='+offset+'&username='+username+'&categories='+category );
  }
  getPost(id: string) {
    return this.http.get<IPost>(this.baseUrl+'posts/' + id);
  }
  updatePost(post: IPost) {
    return this.http.put(this.baseUrl+'posts/' + post.id, post);
  }
  createPost(post: IPost) {
    return this.http.post(this.baseUrl+'posts/', post);
  }
  deletePost(id: string) {
    return this.http.delete(this.baseUrl+'posts/' + id);
  }
  likePost(id: string) {
    return this.http.post(this.baseUrl+'posts/' + id + "/like", {});
  }
  unlikePost(id: string) {
    return this.http.delete(this.baseUrl+'posts/' + id + "/like");
  }
  getCategories(){
    return this.http.get<ICategory[]>(this.baseUrl+'posts/' + 'category');
  }
  getPostStats() {
    return this.http.get<IPostStats[]>(this.baseUrl+'posts/stats');
  }
  getCategoryStats() {
    return this.http.get<ICategoryStats[]>(this.baseUrl+'posts/category/stats');
  }
}
