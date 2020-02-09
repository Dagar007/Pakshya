import { IPostConcise } from '../_models/post';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { PostService } from '../_services/post.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class PostResolver implements Resolve<IPostConcise[]> {
    pageNumber = 1;
    pageSize = 3;
    constructor(private postService: PostService, private route: Router){}
    resolve(route: ActivatedRouteSnapshot) : Observable<IPostConcise[]> {
        return this.postService.getPosts(this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                console.log(error);
                return of(null)
            })
        )
    }
}