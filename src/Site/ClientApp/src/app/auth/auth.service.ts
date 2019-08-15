import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,of } from 'rxjs';
import { AuthCredentials } from "./auth.models";

@Injectable()
export class AuthService {
  constructor(private http: HttpClient) { }

  login(credentials: AuthCredentials): Observable<string> {
    return this.http.post<string>("api/login", credentials);
  }

}
