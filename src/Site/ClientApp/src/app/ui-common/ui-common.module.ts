import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const commonUi = [
  CommonModule,
  MatToolbarModule,
  MatIconModule,
  MatSidenavModule,
  MatListModule,
  MatButtonModule,
  BrowserAnimationsModule
  ];

@NgModule({
  declarations: [],
  imports: commonUi,
  exports: commonUi
})
export class UiCommonModule { }
