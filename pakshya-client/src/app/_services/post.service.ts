import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPost, ICategory, IPostConcise } from '../_models/post';
import { IPostStats, ICategoryStats } from '../_models/sidebarHelper';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import * as fromRoot from './../app.reducer';
import { Store } from '@ngrx/store';
import * as UI from './../_shared/ui.actions';
import { IPhoto } from '../_models/profile';


@Injectable({
  providedIn: 'root'
})
export class PostService {
  private baseUrl = environment.apiUrl;

  catgorySelectedEmitter = new EventEmitter<string>();
  constructor(private http: HttpClient, private store: Store<fromRoot.State>) {}

  getPosts(page?, itemsPerPage?, userParams?): Observable<PaginatedResult<IPostConcise[]>> {

    this.store.dispatch(new UI.StartLoading());
    const paginatedResult: PaginatedResult<IPostConcise[]> = new PaginatedResult<IPostConcise[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    if (userParams != null) {
      params = params.append('category', userParams.category);
    }
    return this.http.get<IPostConcise[]>(this.baseUrl + 'posts/', {observe: 'response', params} )
      .pipe(
        map(response => {

          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          this.store.dispatch(new UI.StopLoading());
          return paginatedResult;
        })
      );
  }
  getPost(id: string) {
    return this.http.get<IPostConcise>(this.baseUrl + 'posts/' + id);
  }
  updatePost(id: string, post: any) {
    return this.http.post(this.baseUrl + 'posts/' + id, post);
  }
  createPost(post: any) {
    return this.http.post(this.baseUrl + 'posts/', post);
  }
  deletePost(id: string) {
    return this.http.delete(this.baseUrl + 'posts/' + id);
  }
  likePost(id: string) {
    return this.http.post(this.baseUrl + 'posts/' + id + '/like', {});
  }
  unlikePost(id: string) {
    return this.http.delete(this.baseUrl + 'posts/' + id + '/like');
  }
  getCategories() {
    return this.http.get<ICategory[]>(this.baseUrl + 'posts/' + 'category');
  }
  getPostStats() {
    return this.http.get<IPostStats[]>(this.baseUrl + 'posts/stats');
  }
  getCategoryStats() {
    return this.http.get<ICategoryStats[]>(this.baseUrl + 'posts/category/stats');
  }
  uploadPhoto(file: any) {
    return this.http.post<IPhoto>(this.baseUrl + 'photos', file);
  }
}
