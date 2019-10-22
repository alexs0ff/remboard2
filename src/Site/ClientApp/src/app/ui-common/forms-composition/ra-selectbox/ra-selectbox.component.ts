import { Component, OnInit,Input,OnDestroy } from '@angular/core';
import { FormGroup } from "@angular/forms";
import { Observable, Subject } from 'rxjs';
import { map, startWith, takeUntil, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { KeyValue } from "../../../app.models";
import { FormControl } from '@angular/forms';
import { DictionaryUtils } from "../../../app.utils";
import { RedirectedErrorStateMatcher, TypedItemsUtils } from "../forms-composition-service";
import { EntityServiceApiFactory, QueryParams, QueryParamsConfigurator } from "../../../features/ra-cruds/ra-cruds.module";
import { PagedResult } from "../../../features/ra-cruds/ra-cruds.models";
import { FormErrorService } from "../form-error-service";
import { RaSelectBox, RaSelectBoxRemoteSource, RaSelectBoxSources, RaSelectBoxItemsSource } from "../../../ra-schema/ra-schema.module";

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
    <mat-error  *ngIf="form.controls[model.id].invalid">
      {{formErrorService.getErrorMessage(form,model.id)}}
    </mat-error>
  </mat-form-field>
</div>
  `,
  styles: [`
.mat-filter {
  position: sticky;
  top: 0;
  z-index: 100;
  font-size: inherit;
  box-shadow: none;
  border-radius: 0;
  padding: 16px;
  box-sizing: border-box;
  border-bottom: 1px solid grey;
  background-color: white;
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

	constructor(private entityServiceApiFactory: EntityServiceApiFactory, public formErrorService: FormErrorService) {
    
  }


  ngOnInit() {

    if (this.isItemsSource(this.model.source)) {
      this.filteredOptions = this.searchControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.filter(value))
        );
    } else if (this.isRemoteSource(this.model.source)) {
      const entitiesService = this.entityServiceApiFactory.getApiService(this.model.source.entitiesName);
      const source: RaSelectBoxRemoteSource = <RaSelectBoxRemoteSource>this.model.source;
      this.filteredOptions = this.searchControl.valueChanges
        .pipe(
          startWith(''),
          debounceTime(200),
          distinctUntilChanged(),
          map((value: string) => this.mapToQuery(value, source)),
          switchMap(query => entitiesService.getWithQuery(query)),
          map(result=>this.pagedResultToKeyValues(result,source))
        );
    }


  }

  ngOnDestroy() {
    this.lifeTimeObject.next(true);
    this.lifeTimeObject.complete();
  }

  private mapToQuery(searchText, source: RaSelectBoxRemoteSource): QueryParams {

    if (source.clientFilter) {
      return null;
    }

    const queryConfigurator = new QueryParamsConfigurator();
    queryConfigurator.setPageSize(source.maxItems || 50);
    queryConfigurator.setCurrentPage(1);

    source.displayColumns.forEach(column => {
      queryConfigurator.orContains(column, searchText);
    });

    const idValue = this.form.controls[source.keyColumn].value;
    if (idValue) {
      queryConfigurator.orEquals(source.keyColumn, idValue);
    }

    return queryConfigurator.toQueryParams();
  }

  private pagedResultToKeyValues(pagedResult: PagedResult, source: RaSelectBoxRemoteSource): KeyValue<string>[] {
    let array = new Array<KeyValue<string>>();
    if (pagedResult.entities && pagedResult.entities.length) {
      
      pagedResult.entities.forEach(entity => {
        let displayValue = "";
        let isFirst = true;
        for (let i = 0; i < source.displayColumns.length; i++) {
          displayValue += entity[source.displayColumns[i]];

          if (isFirst) {
            isFirst = false;
          } else {
            displayValue += " ";
          }
        }

        let keyRaw = entity[source.keyColumn];

        if (this.model.valueKind === "number") {
          keyRaw = parseInt(keyRaw, 10);
        }

        const item: KeyValue<string> = { key: keyRaw, value: displayValue };
        array.push(item);
      });

    }
    return array;
  }

  private filter(value: string): KeyValue<string>[] {
    const filterValue = value.toLowerCase();
    let array = [];
    if (this.isItemsSource(this.model.source)) {
      array = this.model.source.items
        .filter(option => option.value.toLowerCase().includes(filterValue));
    }
    return array;
  }

  isItemsSource(source: RaSelectBoxSources): source is RaSelectBoxItemsSource {
    return source.kind === "items";
  }

  isRemoteSource(source: RaSelectBoxSources): source is RaSelectBoxRemoteSource {
    return source.kind === "remote";
  }

}
