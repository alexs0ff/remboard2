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
import { MessageFlowService } from "../message-flow/message-flow.service";

//from https://stackoverflow.com/a/53379715
@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private messageFlowService:MessageFlowService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.error instanceof Error) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
          this.messageFlowService.showMessage("", error.error.message);

        } else {
          //Auth error
          if (error.status == 401) {
             //401 handled in auth module
          } else {
            console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
            this.messageFlowService.showMessage(String(error.status), "Сетевая ошибка", error.error);
          }
        }
        return throwError(error);
      })
    );
  }
}
