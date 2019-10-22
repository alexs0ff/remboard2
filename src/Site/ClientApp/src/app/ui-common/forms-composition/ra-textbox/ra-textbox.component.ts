import { Component, OnInit,Input } from '@angular/core';
import { FormGroup } from "@angular/forms";
import { RaControls } from "../../../ra-schema/ra-schema.module";
import { FormErrorService } from "../../ui-common.module";

@Component({
  selector: 'ra-textbox',
  template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
    <input matInput placeholder="{{model.label}}" name="{{model.id}}" [formControlName]="model.id">
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error  *ngIf="form.controls[model.id].invalid">
      {{formErrorService.getErrorMessage(form,model.id)}}
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: []
})
export class RaTextboxComponent implements OnInit {

  @Input() model: RaControls;

  @Input()
  form: FormGroup;

	constructor(public formErrorService: FormErrorService) { }

	ngOnInit() {
    
  }

}
