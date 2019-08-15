import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent,
  HttpResponse
} from '@angular/common/http';


import { Observable,throwError } from 'rxjs';
import { retry,catchError } from 'rxjs/operators';

//from https://stackoverflow.com/a/53379715
@Injectable()
export class HttpRetryInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return next.handle(request).pipe(retry(3));
  }
}
