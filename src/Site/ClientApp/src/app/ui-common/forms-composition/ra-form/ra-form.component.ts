import { Component, OnInit,Input } from '@angular/core';
import { RaBaseControl } from "../forms-composition.models";
import { FormsCompositionService } from "../forms-composition-service";
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'ra-form',
  template: `
<form (ngSubmit)="onSubmit()" [formGroup]="form">
  <div *ngFor="let control of controls">
    <ra-control [control]="control"></ra-control>
  </div>
  <div>
    <button type="submit">Save</button>
  </div>
</form>
  `,
  styles: [],
  providers: [FormsCompositionService]
})
export class RaFormComponent implements OnInit {
  @Input() controls: RaBaseControl[] = [];
  form: FormGroup;
  constructor(private service: FormsCompositionService) { }

  ngOnInit() {
    this.form = this.service.toFormGroup(this.controls);
    console.log("dsd", this.controls); 
  }

  onSubmit() {
   
  }
}
