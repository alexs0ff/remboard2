import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, ValidatorFn } from '@angular/forms';
import { RaControl, RaFormLayout, RaFormLayoutRowContent, RaFormLayoutItems, RaFormLayoutHiddenItems } from "./forms-composition.models";


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

  private createFormControl(raControl: RaControl): FormControl {
    const validators = Array<ValidatorFn>();
    if (raControl.validators.required) {
      validators.push(Validators.required);
    }

    if (raControl.validators.maxLength != null) {
      validators.push(Validators.maxLength(raControl.validators.maxLength));
    }

    if (raControl.validators.minLength != null) {
      validators.push(Validators.minLength(raControl.validators.minLength));
    }

    const result = new FormControl(raControl.value || '', validators);
    
    return result;
  }

  private isLayoutItems(content: RaFormLayoutRowContent): content is RaFormLayoutItems {
    return content.kind === "controls";
  }

  private isHiddenItems(content: RaFormLayoutRowContent): content is RaFormLayoutHiddenItems {
    return content.kind === "hidden";
  }
}
