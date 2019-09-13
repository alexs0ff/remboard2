import { Component, OnInit,Input } from '@angular/core';
import { RaControls } from "../forms-composition.models";
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'ra-control',
  template: `
<div [ngSwitch]="control.kind">
<ra-textbox *ngSwitchCase="'textbox'"  [model]="control" [form]="form"></ra-textbox>  
<ra-selectbox *ngSwitchCase="'selectbox'"  [model]="control" [form]="form"></ra-selectbox>  
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
