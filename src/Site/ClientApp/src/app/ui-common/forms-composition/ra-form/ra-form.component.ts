import { Component, OnInit,Input } from '@angular/core';
import { RaControl, RaFormLayout } from "../forms-composition.models";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'ra-form',
  template: `
<form (ngSubmit)="onSubmit()" [formGroup]="form">
  <div *ngFor="let control of controls">
    <ra-control [control]="control" [form]="form"></ra-control>
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
  }

  onSubmit() {
   
  }
}
