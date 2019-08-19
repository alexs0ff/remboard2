import { Component, OnInit, Inject,OnDestroy } from '@angular/core';
import { MAT_DIALOG_DATA,MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store,select } from "@ngrx/store";
import { Subject,Observable } from "rxjs";
import { takeUntil,filter } from "rxjs/operators";
import { authStartLogin } from "../auth.actions";
import { AuthCredentials } from "../auth.models";
import { selectIsAuthenticated, selectLoginSending,selectLoginMessage } from "../auth.selectors";
import { LoginErrorValidator } from "./login-error.validator";

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss'],
  providers: [LoginErrorValidator]
})
export class LoginDialogComponent implements OnInit, OnDestroy {
  loginForm = this.builder.group({ login: ['', { validators: [Validators.required], asyncValidators: [this.loginErrorValidator.validate.bind(this.loginErrorValidator)] }], password: ['', Validators.required] });

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  requestIsSending$: Observable<boolean>;

  loginErrorMessage$:Observable<string>;

  constructor(private builder: FormBuilder,
    private store: Store<{}>,
    matDialogRef: MatDialogRef<LoginDialogComponent>,
    private loginErrorValidator: LoginErrorValidator
  ) {

    store.pipe(select(selectIsAuthenticated), filter(p => p === true), takeUntil(this.lifeTimeObject)).subscribe(result => {
      matDialogRef.close(true);
    });

    this.requestIsSending$ = this.store.pipe(select(selectLoginSending));
    this.loginErrorMessage$ = this.store.pipe(select(selectLoginMessage));
  }

  ngOnInit() {
    
  }

  sendAuth() {
    const login: AuthCredentials = <AuthCredentials>this.loginForm.value;
    this.store.dispatch(authStartLogin({ credentials: login }));
  }

  ngOnDestroy(): void {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }
}
