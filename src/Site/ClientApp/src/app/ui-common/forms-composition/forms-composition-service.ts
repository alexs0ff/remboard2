import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RaBaseControl } from "./forms-composition.models";


@Injectable()
export class FormsCompositionService  {
  constructor() { }

  public toFormGroup(controls: RaBaseControl[]) {
    let group: any = {};

    controls.forEach(control => {
      group[control.key] = control.required
        ? new FormControl(control.value || '', Validators.required)
        : new FormControl(control.value || '');
    });

    return new FormGroup(group);
  }
}
