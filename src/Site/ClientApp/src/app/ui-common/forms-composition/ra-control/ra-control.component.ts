import { Component, OnInit,Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { RaControls } from "../../../ra-schema/ra-schema.module";

@Component({
  selector: 'ra-control',
  template: `
<div [ngSwitch]="control.kind">
<ra-textbox *ngSwitchCase="'textbox'"  [model]="control" [form]="form"></ra-textbox>  
<ra-selectbox *ngSwitchCase="'selectbox'"  [model]="control" [form]="form"></ra-selectbox>  
<ra-textarea *ngSwitchCase="'textarea'"  [model]="control" [form]="form"></ra-textarea>  
<ra-multiselect *ngSwitchCase="'multiselect'"  [model]="control" [form]="form"></ra-multiselect>  
</div>
  `,
  styles: []
})
export class RaControlComponent implements OnInit {
  @Input() control: RaControls;

  @Input()
  form: FormGroup;
  constructor() { }

  ngOnInit() {
  }

}
