import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { IComment } from '../_models/post';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { CommentService } from 'src/app/_services/comment.service';

@Injectable()
export class CommentResolver implements Resolve<IComment[]> {

    constructor(private commentService: CommentService) {}
    pageNumber = 1;
    pageSize = 4;
    resolve(route: ActivatedRouteSnapshot): Observable<IComment[]>  {
        return this.commentService.getComments(route.params['id'], this.pageNumber, this.pageSize).pipe(
            catchError(error => {
                console.log(error);
                return of(null);
            })
        );
    }
}
