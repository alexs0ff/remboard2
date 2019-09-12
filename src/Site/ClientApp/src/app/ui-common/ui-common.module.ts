import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatToolbarModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatExpansionModule } from '@angular/material/expansion';
import { storeMessageValidator } from './store.message.validator';
import { RaTextboxComponent } from './forms-composition/ra-textbox/ra-textbox.component';
import { RaSelectboxComponent } from './forms-composition/ra-selectbox/ra-selectbox.component';
import { RaControlComponent } from './forms-composition/ra-control/ra-control.component';
import { RaFormComponent } from './forms-composition/ra-form/ra-form.component'
import { MatTableModule } from '@angular/material/table';
import { RaServerdataGridComponent } from './list-composition/ra-serverdata-grid/ra-serverdata-grid.component';
import { RaServerDataGridModel } from './list-composition/list-composition.models';
import { RaControl, RaFormLayout, flexExpressions, RaEntityEdit} from './forms-composition/forms-composition.models';
import { MatDividerModule } from '@angular/material/divider';
import { RaEntityEditComponent } from './forms-composition/ra-entity-edit/ra-entity-edit.component';
import { MatTooltipModule } from '@angular/material/tooltip';
const commonUi = [
  MatToolbarModule,
  MatIconModule,
  MatSidenavModule,
  MatSortModule,
  MatListModule,
  MatButtonModule,
  FlexLayoutModule,
  CdkAccordionModule,
  MatInputModule,
  ReactiveFormsModule,
  MatDialogModule,
  MatCardModule,
  MatProgressSpinnerModule,
  MatSnackBarModule,
  MatExpansionModule,
  MatTableModule,
  CommonModule,
  MatPaginatorModule,
  MatDividerModule,
  MatTooltipModule
];

const commonUiImport = [...commonUi];
const commonUiExport = [...commonUi, RaServerdataGridComponent, RaFormComponent, RaEntityEditComponent];

@NgModule({
  declarations: [RaServerdataGridComponent, RaFormComponent, RaControlComponent, RaSelectboxComponent, RaTextboxComponent, RaEntityEditComponent],
  imports: commonUiImport,
  exports: commonUiExport
})
class UiCommonModule { }

export { UiCommonModule, storeMessageValidator, RaServerDataGridModel, RaControl, RaFormLayout, flexExpressions, RaEntityEdit}
