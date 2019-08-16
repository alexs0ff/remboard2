import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UiCommonModule } from "../ui-common/ui-common.module";
import { ErrorSnackComponent } from './error-snack/error-snack.component';


@NgModule({
  declarations: [ErrorSnackComponent],
  imports: [
    CommonModule,
    UiCommonModule
  ],
  entryComponents: [ErrorSnackComponent]
})
class MessageFlowModule { }

export {MessageFlowModule}
