import { createSelector, createFeatureSelector } from '@ngrx/store';
import { AuthState, AuthModuleState, AuthModuleState as IAuthModuleState } from "./auth.reducer";

export const getIsAuthenticated = (state: AuthState) => state.isAuthenticated;
export const getLoginSending = (state: AuthState) => state.loginSending;
export const getLoginMessage = (state: AuthState) => state.loginErrorMessage;
export const getUser = (state: AuthState) => state.user;
export const getAuth = (moduleState: IAuthModuleState) => moduleState.auth;


export const selectIsAuthenticated = createSelector(
  getAuth,
  getIsAuthenticated);

export const selectLoginSending = createSelector(
  getAuth,
  getLoginSending);

export const selectLoginMessage = createSelector(
  getAuth,
  getLoginMessage);

export const selectCurrentUser = createSelector(
  getAuth,
  getUser);
