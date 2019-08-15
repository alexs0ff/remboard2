import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,of } from 'rxjs';
import { AuthCredentials } from "./auth.models";

const tokenKey = "JwtToken";

@Injectable({providedIn:'root'})
export class TokenService {
  
  constructor() { }

  setToken(token: string) {
    localStorage.setItem(tokenKey,token);
  }

  getToken():string {
    return localStorage.getItem(tokenKey);
  }

  clearToken() {
    localStorage.removeItem(tokenKey);
  }

  tokenExists(): boolean {
    const token = localStorage.getItem(tokenKey);
    if (token==null) {
      return false;
    }

    return token.length > 0;
  }

}
