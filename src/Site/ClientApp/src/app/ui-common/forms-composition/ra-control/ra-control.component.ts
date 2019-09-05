import { Component, OnInit,Input } from '@angular/core';
import { RaBaseControl } from "../forms-composition.models";

@Component({
  selector: 'ra-control',
  template: `
<div [ngSwitch]="control.controlType">
<ra-textbox *ngSwitchCase="'TEXTBOX'"  [model]="control"></ra-textbox>  
<ra-selectbox *ngSwitchCase="'SELECTBOX'"  [model]="control"></ra-selectbox>  
</div>
  `,
  styles: []
})
export class RaControlComponent implements OnInit {
  @Input() control: RaBaseControl;
  constructor() { }

  ngOnInit() {
  }

}
