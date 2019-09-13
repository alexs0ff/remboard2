import { Component, OnInit,Input,OnDestroy } from '@angular/core';
import { RaSelectBox, RaSelectBoxSources, RaSelectBoxItemsSource } from "../forms-composition.models";
import { FormGroup } from "@angular/forms";
import { Observable, Subject } from 'rxjs';
import { map, startWith,takeUntil } from 'rxjs/operators';
import { KeyValue } from "../../../app.models";
import { FormControl } from '@angular/forms';
import { DictionaryUtils } from "../../../app.utils";
import { RedirectedErrorStateMatcher, TypedItemsUtils } from "../forms-composition-service";

@Component({
  selector: 'ra-selectbox',
  template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
    <mat-label>{{model.label}}</mat-label>
    <mat-select [formControlName]="model.id">
<div class="mat-filter">
      <input matInput type="text" [formControl]="searchControl" placeholder="Поиск…">
</div>
      <mat-option>--</mat-option>
      <mat-option *ngFor="let option of filteredOptions | async" [value]="option.key">
        {{option.value}}
      </mat-option>
    </mat-select>
    <mat-hint>{{model.hint}}</mat-hint>
    <mat-error>
      Error text
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: [`
.mat-filter {
  position: -webkit-sticky;
  position: sticky;
  top: 0;
  z-index: 100;
  font-size: inherit;
  box-shadow: none;
  border-radius: 0;
  padding: 16px;
  box-sizing: border-box;
  border-bottom: 1px solid grey;
}
`]
})
export class RaSelectboxComponent implements OnInit, OnDestroy {

  private lifeTimeObject: Subject<boolean> = new Subject<boolean>();

  @Input() model: RaSelectBox;
  searchControl = new FormControl();

  @Input()
  form: FormGroup;


  filteredOptions: Observable<KeyValue<string>[]>;

  constructor() { }

  ngOnInit() {

    this.filteredOptions = this.searchControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this.filter(value))
    );

  }

  ngOnDestroy() {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }

  private filter(value: string): KeyValue<string>[] {
    const filterValue = value.toLowerCase();
    let array = [];
    if (this.isItemsSource(this.model.source)) {
      array = DictionaryUtils.toArray(this.model.source.items)
        .filter(option => option.value.toLowerCase().includes(filterValue));

      TypedItemsUtils.changeKeyTypeInArray(array, this.model.valueKind);
    }
    

    return array;
  }

  isItemsSource(source: RaSelectBoxSources): source is RaSelectBoxItemsSource {
    return source.kind === "items";
  }

}
