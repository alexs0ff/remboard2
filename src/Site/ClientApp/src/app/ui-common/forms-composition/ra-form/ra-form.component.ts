import { Component, OnInit,Input } from '@angular/core';
import { RaControl, RaFormLayout } from "../forms-composition.models";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'ra-form',
  template: `
<form (ngSubmit)="onSubmit()" [formGroup]="form">

  <div *ngFor="let row of layout.rows">
    <div [ngSwitch]="row.content.kind">  
      <h3 *ngSwitchCase="'caption'" class="ra-fields-caption">{{row.content.title}}</h3>
      <mat-divider *ngSwitchCase="'divider'"></mat-divider>
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
  providers: [FormsCompositionService]
})
export class RaFormComponent implements OnInit {
  @Input() layout: RaFormLayout;
  form: FormGroup;
  constructor(private service: FormsCompositionService) { }
  ngOnInit() {
    this.form = this.service.toFormGroup(this.layout);
    console.log("form",this.form);
    console.log("layout", this.layout);
  }

  onSubmit() {
   
  }
}
