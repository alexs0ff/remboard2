import { createSelector, createFeatureSelector } from '@ngrx/store';
import { AuthState, AuthModuleState } from "./auth.reducer";

export const getIsAuthenticated = (state: AuthState) => state.isAuthenticated;

export const selectIsAuthenticated = createSelector(
  (moduleState: AuthModuleState) => moduleState.auth,
  getIsAuthenticated);
