import { Action, createReducer, on } from '@ngrx/store'
import * as AuthActions from "./auth.actions"
import authLogOut = AuthActions.authLogOut;


export interface AuthState {
  isAuthenticated: boolean;
  loginSending:boolean;

}

export interface AuthModuleState {
  auth:AuthState;
}

export const featureKey = "auth";

export const initialState: AuthState = { isAuthenticated: false, loginSending:false};


const authReducer = createReducer(
  initialState,
  on(AuthActions.authStartLogin, (state: AuthState) => ({ ...state, loginSending:true })),
  on(AuthActions.authLoginError, (state: AuthState) => ({ ...state, loginSending:false })),
  on(AuthActions.authSignedIn, (state: AuthState) => ({ ...state, isAuthenticated: true, loginSending:false })),
  on(authLogOut, (state: AuthState) => ({ ...state, isAuthenticated: false}))
);

export function reducer(state: AuthState | undefined, action: Action) {
  return authReducer(state, action);
}


