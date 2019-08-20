import { Action, createReducer, on } from '@ngrx/store'
import * as AuthActions from "./auth.actions"
import authLogOut = AuthActions.authLogOut;
import { UserInfo } from "./auth.models";


export interface AuthState {
  isAuthenticated: boolean;
  loginSending:boolean;
  loginErrorMessage: string;
  user: UserInfo;
}

export interface AuthModuleState {
  auth:AuthState;
}

export const featureKey = "auth";

const emptyUser: UserInfo = null;

export const initialState: AuthState = { isAuthenticated: false, loginSending: false, loginErrorMessage: null, user: emptyUser };


const authReducer = createReducer(
  initialState,
  on(AuthActions.authStartLogin, (state: AuthState) => ({ ...state, loginSending: true, loginErrorMessage:null })),
  on(AuthActions.authLoginError, (state: AuthState, { message }) => ({ ...state, loginSending: false, loginErrorMessage:message })),
  on(AuthActions.authSignedIn, (state: AuthState, { token, user }) => ({ ...state, isAuthenticated: true, loginSending: false, user: user })),
  on(authLogOut, (state: AuthState) => ({ ...state, user:emptyUser, isAuthenticated: false }))
);

export function reducer(state: AuthState | undefined, action: Action) {
  return authReducer(state, action);
}


