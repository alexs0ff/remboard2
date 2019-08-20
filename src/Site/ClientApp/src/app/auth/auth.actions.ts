import { createAction,props } from '@ngrx/store'
import { AuthCredentials, UserInfo } from "./auth.models";


export const authSignedIn = createAction('[Auth Page] SignedIn', props<{token:string,user:UserInfo}>());
export const authLoginError = createAction('[Auth Page] LoginError', props<{ message: string }>());
export const authLogOut = createAction('[Auth Page] LogOut');
export const authStartLogin = createAction('[Auth Page] Start Auth', props<{ credentials: AuthCredentials}>());

