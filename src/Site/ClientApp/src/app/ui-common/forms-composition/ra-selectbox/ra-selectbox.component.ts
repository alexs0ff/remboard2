import { Component, OnInit,Input } from '@angular/core';
import { RaControl } from "../forms-composition.models";
import { FormGroup } from "@angular/forms";

@Component({
  selector: 'ra-selectbox',
  template: `
    <p>
      ra-select-box works!
    </p>
  `,
  styles: []
})
export class RaSelectboxComponent implements OnInit {
  @Input() model: RaControl;


  @Input()
  form: FormGroup;
  constructor() { }

  ngOnInit() {
  }

}
