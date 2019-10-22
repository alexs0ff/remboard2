import { Component, OnInit,Input } from '@angular/core';
import { FormGroup } from "@angular/forms";
import { FormErrorService } from "../form-error-service";
import { RaControls } from "../../../ra-schema/ra-schema.module";

@Component({
  selector: 'ra-textarea',
  template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
    <textarea matInput placeholder="{{model.label}}" name="{{model.id}}" [formControlName]="model.id"></textarea>
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error  *ngIf="form.controls[model.id].invalid">
      {{formErrorService.getErrorMessage(form,model.id)}}
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: []
})
export class RaTextareaComponent implements OnInit {

  @Input() model: RaControls;

  @Input()
  form: FormGroup;

	constructor(public formErrorService: FormErrorService) { }

	ngOnInit() {
    
  }

}
