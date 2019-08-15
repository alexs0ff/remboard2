import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UiCommonModule } from "../ui-common/ui-common.module";
import { LoginDialogComponent } from "./login-dialog/login-dialog.component";
import { LoginButtonComponent } from './login-button/login-button.component';
import { StoreModule } from '@ngrx/store';

import * as AuthReducer from "./auth.reducer"

@NgModule({
  declarations: [LoginDialogComponent, LoginButtonComponent],
  imports: [
    CommonModule,
    UiCommonModule,
    StoreModule.forFeature(AuthReducer.featureKey, AuthReducer.reducer),
  ],
  exports: [LoginButtonComponent],
  entryComponents: [LoginDialogComponent]
})
export class AuthModule { }
