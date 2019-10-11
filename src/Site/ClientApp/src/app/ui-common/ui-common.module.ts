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
import { RaFormLayout, flexExpressions, RaEntityEdit} from './forms-composition/forms-composition.models';
import { MatDividerModule } from '@angular/material/divider';
import { RaEntityEditComponent } from './forms-composition/ra-entity-edit/ra-entity-edit.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FormsCompositionService } from "./forms-composition/forms-composition-service";
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { RaAutocompleteboxComponent } from "./forms-composition/ra-autocompletebox/ra-autocompletebox.component";
import { MatSelectModule } from '@angular/material/select';
import { RaEntityEditRemoveDialog } from "./forms-composition/ra-entity-edit/ra-entity-edit-remove-dialog";
import { FormErrorService } from "./forms-composition/form-error-service";
import { RaTextareaComponent } from "./forms-composition/ra-textarea/ra-textarea.component";
import { RaGridFilterComponent } from './list-composition/ra-grid-filter/ra-grid-filter.component';


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
  MatTooltipModule,
  MatAutocompleteModule,
  MatSelectModule
];

const commonUiImport = [...commonUi];
const commonUiExport = [...commonUi, RaServerdataGridComponent, RaFormComponent, RaEntityEditComponent];

@NgModule({
  entryComponents: [RaEntityEditRemoveDialog],
	declarations: [RaServerdataGridComponent,
		RaFormComponent,
		RaControlComponent,
		RaTextareaComponent,
		RaAutocompleteboxComponent,
		RaSelectboxComponent,
		RaTextboxComponent,
		RaEntityEditComponent,
		RaEntityEditRemoveDialog,
		RaGridFilterComponent],
  imports: commonUiImport,
  exports: commonUiExport,
	providers: [FormsCompositionService, FormErrorService]
})
class UiCommonModule { }

export { UiCommonModule, storeMessageValidator, RaServerDataGridModel, RaFormLayout, flexExpressions, RaEntityEdit, FormErrorService}
