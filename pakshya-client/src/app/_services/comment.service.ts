import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PaginatedResult } from '../shared/_models/pagination';
import { IComment } from '../shared/_models/post';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getComments(postId: string, page?, itemsPerPage?, userParams?): Observable<PaginatedResult<IComment[]>> {
    const paginatedResult: PaginatedResult<IComment[]> = new PaginatedResult<IComment[]>();

    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    if (userParams != null) {
      params = params.append('category', userParams.category);
    }

    return this.http.get<IComment[]>(this.baseUrl + 'comment/' + postId, {observe: 'response', params}).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );

  }

  deleteComment(id: string) {
    return this.http.delete(this.baseUrl + 'comment/' + id);
  }

  likeComment(id: string) {
    return this.http.post(this.baseUrl + 'comment/' + id + '/like', {});
  }
  unlikeComment(id: string) {
    return this.http.delete(this.baseUrl + 'comment/' + id + '/like');
  }

}
