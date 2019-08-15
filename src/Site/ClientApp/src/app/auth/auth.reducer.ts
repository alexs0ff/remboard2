import { Action, createReducer, on } from '@ngrx/store'
import * as AuthActions from "./auth.actions"
import authLogOut = AuthActions.authLogOut;


export interface AuthState {
  isAuthenticated: boolean;

}

export interface AuthModuleState {
  auth:AuthState;
}

export const featureKey = "auth";

export const initialState: AuthState = { isAuthenticated:false};


const authReducer = createReducer(
  initialState,
  on(AuthActions.authSignedIn, (state: AuthState) => ({ ...state, isAuthenticated:true })),
  on(authLogOut, (state: AuthState) => ({ ...state, isAuthenticated: false}))
);

export function reducer(state: AuthState | undefined, action: Action) {
  return authReducer(state, action);
}


