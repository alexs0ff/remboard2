import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpErrorInterceptor } from "./http-client-error.interceptor";
import { HttpRetryInterceptor } from "./http-client-retry.interceptor";

/** Http interceptor providers in outside-in order */
export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: HttpRetryInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },//responses processing in reverse order
];
