import { Component, OnInit,Input } from '@angular/core';
import { Location } from '@angular/common';
import { RaEntityEdit, RaFormLayout } from "../forms-composition.models";

@Component({
  selector: 'ra-entity-edit',
  templateUrl: './ra-entity-edit.component.html',
  styleUrls: ['./ra-entity-edit.component.scss']
})
export class RaEntityEditComponent implements OnInit {
  @Input() model: RaEntityEdit;
  layout: RaFormLayout;
  constructor(private location: Location) { }

  ngOnInit() {
    this.layout = this.model.layout;
  }

  goBack() {
    this.location.back();
  }
}
