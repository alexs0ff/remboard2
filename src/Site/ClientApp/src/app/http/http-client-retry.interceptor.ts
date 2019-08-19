import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent,
  HttpResponse
} from '@angular/common/http';


import { Observable,throwError,of } from 'rxjs';
import { retry, catchError, retryWhen, switchMap, scan, takeWhile, delay, concat } from 'rxjs/operators';

//from https://stackoverflow.com/questions/47261758/angular-httpclient-request-with-retry-and-delay-is-sending-an-extra-request-then
@Injectable()
export class HttpRetryInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return next.handle(request).pipe(retryWhen(errors => errors.pipe(
      switchMap((error) => {
        if (error.status !== 401) {
          return of(error.status);
        }
        return throwError({ message: error.error.message || 'Notification.Core.loginError' });
      }),
      scan(acc => acc + 1, 0),
      takeWhile(acc => acc < 3),
      delay(1000),
      concat(throwError({ message: 'Notification.Core.networkError' }))
    )));
    //return next.handle(request).pipe(retry(3));
  }
}
