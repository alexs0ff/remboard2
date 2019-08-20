import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,of } from 'rxjs';
import { AuthCredentials, UserInfo } from "./auth.models";
import { JwtHelperService } from "@auth0/angular-jwt";

const tokenKey = "JwtToken";

enum Claims {
  Name = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
  Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

}

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

  toUser(token: string): UserInfo {
    const helper = new JwtHelperService();
    const decodedToken = helper.decodeToken(token);
    const user: UserInfo = {
      name: decodedToken[Claims.Name],
      roles:[]
    };

    if (decodedToken[Claims.Role] != null) {
      if (Array.isArray(decodedToken[Claims.Role])) {
        for (var i = 0; i < decodedToken[Claims.Role].length; i++) {
          user.roles.push(decodedToken[Claims.Role][i]);
        }
      } else {
        user.roles.push(decodedToken[Claims.Role]);
      }
     
    }

    return user;
  }

}
