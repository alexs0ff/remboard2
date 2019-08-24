import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UiCommonModule } from "../ui-common/ui-common.module";
import { LoginDialogComponent } from "./login-dialog/login-dialog.component";
import { LoginButtonComponent } from './login-button/login-button.component';
import { StoreModule } from '@ngrx/store';

import * as AuthReducer from "./auth.reducer"
import { LoginDialogService } from "./login-dialog/login-dialog.service";
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from "./auth.service";
import { TokenService } from "./token.service";
import { AuthEffects } from "./auth.effects";
import { UserInfo } from "./auth.models";
import { selectCurrentUser, selectIsAuthenticated } from "./auth.selectors";
import { authSignedIn } from './auth.actions';

@NgModule({
  declarations: [LoginDialogComponent, LoginButtonComponent],
  imports: [
    CommonModule,
    UiCommonModule,
    StoreModule.forFeature(AuthReducer.featureKey, AuthReducer.reducer),
    HttpClientModule,
  ],
  exports: [LoginButtonComponent],
  entryComponents: [LoginDialogComponent],
  providers: [LoginDialogService, AuthService]
})
class AuthModule { }

export { AuthModule, AuthEffects, TokenService, selectCurrentUser, UserInfo, selectIsAuthenticated, authSignedIn};
