import { Component, OnInit } from '@angular/core';
import { Store, select } from "@ngrx/store";
import {Observable} from "rxjs"
import { authSignedIn, authLogOut as authSignedOut } from "../auth.actions";
import { selectIsAuthenticated } from "../auth.selectors";

@Component({
  selector: 'login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.scss']
})
export class LoginButtonComponent implements OnInit {

  isAuthenticated$:Observable<boolean>;

  constructor(private store: Store<{}>) {
    this.isAuthenticated$ = store.pipe(select(selectIsAuthenticated));
  }

  ngOnInit() {

  }

  startLogIn() {
    this.store.dispatch(authSignedIn());
  }

  startLogOut() {
    this.store.dispatch(authSignedOut());
  }
}
