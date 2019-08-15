import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UiCommonModule } from "../ui-common/ui-common.module";
import { LoginDialogComponent } from "./login-dialog/login-dialog.component";


@NgModule({
  declarations: [LoginDialogComponent],
  imports: [
    CommonModule,
    UiCommonModule
  ],
  entryComponents: [LoginDialogComponent]
})
export class AuthModule { }
