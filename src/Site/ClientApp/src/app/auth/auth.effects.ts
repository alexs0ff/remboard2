import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, exhaustMap, switchMap, catchError, concatMap, withLatestFrom, tap } from 'rxjs/operators';
import { EMPTY,of } from 'rxjs';
import { authStartLogin, authSignedIn, authLogOut, authLoginError } from "./auth.actions";
import { AuthService } from "./auth.service";
import { TokenService } from "./token.service";

/*
 switchMap
Cancels the current subscription/request and can cause race condition
Use for get requests or cancelable requests like searches

concatMap
Runs subscriptions/requests in order and is less performant
Use for get, post and put requests when order is important

mergeMap
Runs subscriptions/requests in parallel
Use for put, post and delete methods when order is not important

exhaustMap
Ignores all subsequent subscriptions/requests until it completes
Use for login when you do not want more requests until the initial one is complete

*
 */
@Injectable()
export class AuthEffects {
  startLogins$ = createEffect(() => this.actions$.pipe(
    ofType(authStartLogin),
    exhaustMap((e) => this.authService.login(e.credentials)
        .pipe(
          map(token => {
            const userInfo = this.tokenService.toUser(token.access_token);
            return authSignedIn({ token: token.access_token, user: userInfo });
          }),
          catchError((error) => {

            let message: string = "Неизвестна ошибка";

            switch (error.status) {
              case 401:
                message = "Ошибочный логин или пароль";
                break;
              case 404:
                message = "Сервер остановлен";
                break;
            default:
            }
            

            return of(authLoginError({ message: message}));
          })
        ))
    )
  );

  authSignedIns$ = createEffect(() => this.actions$.pipe(
    ofType(authSignedIn), tap(a => this.tokenService.setToken(a.token))
  ), { dispatch: false }
  );

  authLogOuts$ = createEffect(() => this.actions$.pipe(
    ofType(authLogOut), tap(a => this.tokenService.clearToken())
    ), { dispatch: false }
  );

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private tokenService: TokenService
  ) {

  }
}
