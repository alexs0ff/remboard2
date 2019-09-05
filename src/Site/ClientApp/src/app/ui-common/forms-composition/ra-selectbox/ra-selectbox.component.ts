import { Component, OnInit,Input } from '@angular/core';
import { RaSelectbox } from "../forms-composition.models";

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
  @Input() model: RaSelectbox;
  constructor() { }

  ngOnInit() {
  }

}
