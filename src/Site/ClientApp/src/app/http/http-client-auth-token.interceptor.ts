import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent,
  HttpResponse
} from '@angular/common/http';


import { Observable, EMPTY, throwError,of } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { TokenService } from "../auth/auth.module";

//from https://blog.angularindepth.com/top-10-ways-to-use-interceptors-in-angular-db450f8a62d6
@Injectable()
export class HttpAuthTokenInterceptor implements HttpInterceptor {

  private AUTH_HEADER = "Authorization";

  constructor(private tokenService:TokenService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!request.headers.has('Content-Type')) {
      request = request.clone({
        headers: request.headers.set('Content-Type', 'application/json')
      });
    }

    request = this.addAuthenticationToken(request);
    return next.handle(request);
  }

  private addAuthenticationToken(request: HttpRequest<any>): HttpRequest<any> {
    // If we do not have a token yet then we should not set the header.
    // Here we could first retrieve the token from where we store it.
    if (!this.tokenService.tokenExists()) {
      return request;
    }
    // If you are calling an outside domain then do not add the token.
    //TODO:
    //if (!request.url.match(/www.mydomain.com\//)) {
    //  return request;
    //}

    return request.clone({
      headers: request.headers.set(this.AUTH_HEADER, "Bearer " + this.tokenService.getToken())
    });
  }
}
