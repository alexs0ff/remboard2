import { Component, OnInit,Input } from '@angular/core';
import { RaControl } from "../forms-composition.models";
import { FormGroup } from "@angular/forms";

@Component({
  selector: 'ra-textbox',
  template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
    <input matInput placeholder="{{model.label}}" name="{{model.id}}" [formControlName]="model.id">
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error>
      Error text
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: []
})
export class RaTextboxComponent implements OnInit {

  @Input() model: RaControl;

  @Input()
  form: FormGroup;

  constructor() { }

  ngOnInit() {
  }

}
