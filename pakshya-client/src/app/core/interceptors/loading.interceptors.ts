import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, identity } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { Injectable } from '@angular/core';
import { delay, finalize } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
    constructor(private busyService: BusyService) {}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.busyService.busy();
        return next.handle(req).pipe(
            (environment.production ? identity :  delay(1000)),
            finalize(() => {
                this.busyService.idle();
            })
        );
    }
}
