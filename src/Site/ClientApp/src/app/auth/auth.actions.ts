import { createAction,props } from '@ngrx/store'
import { AuthCredentials } from "./auth.models";


export const authSignedIn = createAction('[Auth Page] SignedIn');
export const authLogOut = createAction('[Auth Page] LogOut');
export const authStart = createAction('[Auth Page] Start Auth', props<{ credentials: AuthCredentials}>());

