import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { AuthCredentials } from "./auth.models";

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) { }

  login(credentials: AuthCredentials): Observable<{ access_token: string }> {
    return this.http.post<{ access_token:string }>("api/login", credentials);
  }

}
