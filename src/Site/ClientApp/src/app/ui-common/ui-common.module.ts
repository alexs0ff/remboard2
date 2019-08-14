import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { MatInputModule } from '@angular/material/input';

const commonUi = [
  CommonModule,
  MatToolbarModule,
  MatIconModule,
  MatSidenavModule,
  MatListModule,
  MatButtonModule,
  BrowserAnimationsModule,
  FlexLayoutModule,
  CdkAccordionModule,
  MatInputModule
  ];

@NgModule({
  declarations: [],
  imports: commonUi,
  exports: commonUi
})
export class UiCommonModule { }
