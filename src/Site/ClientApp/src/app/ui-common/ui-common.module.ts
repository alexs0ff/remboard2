import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule } from '@angular/material/expansion';
import { storeMessageValidator } from './store.message.validator'

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
  MatInputModule,
  ReactiveFormsModule,
  MatDialogModule,
  MatCardModule,
  MatProgressSpinnerModule,
  MatSnackBarModule,
  MatExpansionModule
  ];

@NgModule({
  declarations: [],
  imports: commonUi,
  exports: commonUi
})
class UiCommonModule { }

export { UiCommonModule, storeMessageValidator}
