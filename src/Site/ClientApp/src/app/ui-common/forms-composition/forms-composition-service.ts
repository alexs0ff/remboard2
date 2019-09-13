import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, ValidatorFn, FormGroupDirective, NgForm, AbstractControl  } from '@angular/forms';
import { RaControls, RaFormLayout, RaFormLayoutRowContent, RaFormLayoutItems, RaFormLayoutHiddenItems, RaTextBox } from "./forms-composition.models";
import { ErrorStateMatcher } from '@angular/material/core';


@Injectable()
export class FormsCompositionService  {
  constructor() { }

  public toFormGroup(layout: RaFormLayout) {
    let group: any = {};

    layout.rows.forEach(row => {
      if (this.isLayoutItems(row.content)) {
        row.content.items.forEach(item => {
          group[item.control.id] = this.createFormControl(item.control);  
        });
      }

      if (this.isHiddenItems(row.content)) {
        row.content.items.forEach(hitem => {
          group[hitem] = new FormControl('');
        });
      }
      
    });

    return new FormGroup(group);
  }

  private createFormControl(raControl: RaControls): FormControl {
    const validators = Array<ValidatorFn>();
    if (raControl.validators.required) {
      validators.push(Validators.required);
    }

    if (this.isRaTextBox(raControl)) {
      if (raControl.validators.maxLength != null) {
        validators.push(Validators.maxLength(raControl.validators.maxLength));
      }

      if (raControl.validators.minLength != null) {
        validators.push(Validators.minLength(raControl.validators.minLength));
      }
    }
    const result = new FormControl(raControl.value || '', validators);
    
    return result;
  }

  private isRaTextBox(raControl: RaControls): raControl is RaTextBox {
    return raControl.kind === "textbox";
  }

  private isLayoutItems(content: RaFormLayoutRowContent): content is RaFormLayoutItems {
    return content.kind === "controls";
  }

  private isHiddenItems(content: RaFormLayoutRowContent): content is RaFormLayoutHiddenItems {
    return content.kind === "hidden";
  }
}

export class RedirectedErrorStateMatcher implements ErrorStateMatcher {
  constructor(private parentControl: AbstractControl) {}

  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(this.parentControl && this.parentControl.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
