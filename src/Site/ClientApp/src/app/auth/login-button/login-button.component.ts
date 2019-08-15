import { Component, OnInit } from '@angular/core';
import { Store, select } from "@ngrx/store";
import {Observable} from "rxjs"
import { authSignedIn, authLogOut as authSignedOut } from "../auth.actions";
import { selectIsAuthenticated } from "../auth.selectors";
import { LoginDialogService } from "../login-dialog/login-dialog.service";

@Component({
  selector: 'login-button',
  templateUrl: './login-button.component.html',
  styleUrls: ['./login-button.component.scss']
})
export class LoginButtonComponent implements OnInit {

  isAuthenticated$:Observable<boolean>;

  constructor(private store: Store<{}>, private loginDialogService: LoginDialogService) {
    this.isAuthenticated$ = store.pipe(select(selectIsAuthenticated));
  }

  ngOnInit() {

  }

  startLogIn() {
    this.loginDialogService.open();
  }

  startLogOut() {
    this.store.dispatch(authSignedOut());
  }
}
