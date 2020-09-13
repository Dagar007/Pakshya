import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpErrorResponse, HTTP_INTERCEPTORS, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor() {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401) {
          return throwError(error.statusText);
        }
        if (error instanceof HttpErrorResponse) {
            if (error.status === 500) {
                return throwError(error.error.errors);
            }
            const serverError = error.error;
            let modelStateError = '';
            if (serverError.errors && typeof serverError.errors === 'object') {
                for (const key in serverError.errors) {
                    if (serverError.errors[key]) {
                        modelStateError += serverError.errors[key] + '\n';
                    }
                }
            }
            return throwError(modelStateError || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
    provide : HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
