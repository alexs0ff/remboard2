import { Component, OnInit,Input,OnDestroy } from '@angular/core';
import { FormGroup } from "@angular/forms";
import { Observable, Subject } from 'rxjs';
import { map, startWith,takeUntil } from 'rxjs/operators';
import { KeyValue } from "../../../app.models";
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { DictionaryUtils } from "../../../app.utils";
import { RedirectedErrorStateMatcher } from "../forms-composition-service";
import { RaAutocompleteBox } from "../../../ra-schema/ra-schema.module";

@Component({
  selector: 'ra-autocompletebox',
  template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
    <input type="hidden" name="{{model.id}}" [formControlName]="model.id"/>
    <input matInput placeholder="{{model.label}}" [matAutocomplete]="auto" [formControl]="myControl" [errorStateMatcher]="matcher"/>
    <button mat-button matSuffix mat-icon-button aria-label="Clear" (click)="onClear()">
      <mat-icon>close</mat-icon>
    </button>
    <mat-autocomplete #auto="matAutocomplete" (optionSelected)="onOptionSelected($event)">
      <mat-option *ngFor="let option of filteredOptions | async" [value]="option.value" [id]="option.key">
        {{option.value}}
      </mat-option>
    </mat-autocomplete>
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error>
      Error text
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: []
})
export class RaAutocompleteboxComponent implements OnInit, OnDestroy {

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  @Input() model: RaAutocompleteBox;
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

  onClear() {
    this.myControl.setValue('');
    this.form.controls[this.model.id].setValue(null);
  }
  onOptionSelected(event: MatAutocompleteSelectedEvent) {
    console.log("selected",event.option);
    this.form.controls[this.model.id].setValue(event.option.id);
  }

  onBlur() {
    //const currentValue = this.form.controls[this.model.id].value;
    //if (currentValue) {
    //  const currentTitle = this.model.items[currentValue];
    //  this.myControl.setValue(currentTitle);
    //}
  }
}
