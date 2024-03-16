import { IPostConcise } from '../_models/post';
import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from '@angular/router';
import { of } from 'rxjs';
import { inject } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { PostService } from 'src/app/post/post.service';
import { PaginatedResult } from '../_models/pagination';

export const PostResolver: ResolveFn<PaginatedResult<IPostConcise[]>> =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        const pageNumber = 1;
        const pageSize = 3;

        return inject(PostService).getPosts(pageNumber, pageSize)
            .pipe(
                catchError(error => {
                    console.log(error);
                    return of(null);
                })
            );
    };