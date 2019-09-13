import { Component, OnInit,Input } from '@angular/core';
import { RaControls, RaFormLayout } from "../forms-composition.models";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'ra-form',
  template: `
<form [formGroup]="form">
  <div *ngFor="let row of layout.rows">
    <div [ngSwitch]="row.content.kind">  
      <h3 *ngSwitchCase="'caption'" class="ra-fields-caption">{{row.content.title}}</h3>
      <mat-divider *ngSwitchCase="'divider'"></mat-divider>
      <ng-template *ngSwitchCase="'hidden'">
          <input type="hidden" *ngFor="let hitem of row.content.items" [formControlName]="hitem"/>
      </ng-template>
      <div *ngSwitchCase="'controls'"
          fxLayout="row wrap" 
          fxLayout.lt-sm="column" 
          fxLayoutGap="32px" 
          fxLayoutAlign="flex-start">
        <div *ngFor="let item of row.content.items"
          [fxFlex]="item.flexExpression.fxFlexCommonExpression"
          [fxFlex.lt-md]="item.flexExpression.fxFlexLtmdExpression"
          [fxFlex.lt-sm]="item.flexExpression.fxFlexLtsmExpression"
          >
          <ra-control [control]="item.control" [form]="form"></ra-control>
        </div>        
      </div>
    </div>
  </div>
</form>
  `,
  styles: [],
  providers: []
})
export class RaFormComponent implements OnInit {
  @Input() layout: RaFormLayout;

  @Input()
  form: FormGroup;
  constructor() { }
  ngOnInit() {
    
  }

  onSubmit() {
   
  }
}
