import { Component, OnInit,Input } from '@angular/core';
import { RaTextbox } from "../forms-composition.models";

@Component({
  selector: 'ra-textbox',
  template: `
  <mat-form-field>
    <input [attr.for]="model.key" matInput placeholder="{{model.label}}" name="{{model.key}}" [formControlName]="model.key">
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error>
      Error text
    </mat-error>
  </mat-form-field>
  `,
  styles: []
})
export class RaTextboxComponent implements OnInit {

  @Input() model: RaTextbox;

  constructor() { }

  ngOnInit() {
  }

}
