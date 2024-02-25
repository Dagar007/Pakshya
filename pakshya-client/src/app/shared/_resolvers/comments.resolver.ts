import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, ResolveFn } from '@angular/router';
import { IComment } from '../_models/post';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable, inject } from '@angular/core';
import { CommentService } from 'src/app/post/comment.service';
import { PaginatedResult } from '../_models/pagination';

export const CommentResolver: ResolveFn<PaginatedResult<IComment[]>> =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        const pageNumber = 1;
        const pageSize = 4;

        return inject(CommentService).getComments(route.params['id'], pageNumber, pageSize)
            .pipe(
                catchError(error => {
                    console.log(error);
                    return of(null);
                })
            );
    };
