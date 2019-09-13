import { Component, OnInit,Input,OnDestroy } from '@angular/core';
import { RaSelectBox } from "../forms-composition.models";
import { FormGroup } from "@angular/forms";
import { Observable, Subject } from 'rxjs';
import { map, startWith,takeUntil } from 'rxjs/operators';
import { KeyValue } from "../../../app.models";
import { FormControl } from '@angular/forms';
import { DictionaryUtils } from "../../../app.utils";
import { RedirectedErrorStateMatcher } from "../forms-composition-service";

@Component({
  selector: 'ra-selectbox',
  template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
    
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error>
      Error text
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: []
})
export class RaSelectboxComponent implements OnInit, OnDestroy {

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  @Input() model: RaSelectBox;
  myControl = new FormControl();

  @Input()
  form: FormGroup;


  filteredOptions: Observable<KeyValue<string>[]>;

  matcher: RedirectedErrorStateMatcher;

  constructor() { }

  ngOnInit() {

    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this.filter(value))
    );

    this.matcher = new RedirectedErrorStateMatcher(this.form.controls[this.model.id]);
  }

  ngOnDestroy() {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }

  private filter(value: string): KeyValue<string>[] {
    const filterValue = value.toLowerCase();
    return DictionaryUtils.toArray(this.model.items).filter(option => option.value.toLowerCase().includes(filterValue));
  }

  onBlur() {
    //const currentValue = this.form.controls[this.model.id].value;
    //if (currentValue) {
    //  const currentTitle = this.model.items[currentValue];
    //  this.myControl.setValue(currentTitle);
    //}
  }
}
