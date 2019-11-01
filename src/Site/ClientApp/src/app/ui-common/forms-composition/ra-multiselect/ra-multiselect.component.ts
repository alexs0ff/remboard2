import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { FormGroup } from "@angular/forms";
import { Observable, Subject } from 'rxjs';
import { map, startWith, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { MatChipInputEvent } from '@angular/material';

import { FormControl } from '@angular/forms';
import { RaMultiselect, RaMultiselectRemoteSource, RaMultiselectSources } from "../../../ra-schema/ra-schema.module";
import { EntityServiceApiFactory, QueryParams, QueryParamsConfigurator, PagedResult } from "../../../features/ra-cruds/ra-cruds.module";
import { FormErrorService } from "../../ui-common.module";
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { HttpClient } from '@angular/common/http';
import { RaUtils } from "../../../features/ra-cruds/ra-cruds.utils";
import { HttpParams } from "@angular/common/http/http";


@Component({
	selector: 'ra-multiselect',
	template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
	<input type="hidden" name="{{model.id}}" [formControlName]="model.id"/>
    <mat-label>{{model.label}}</mat-label>
	<mat-chip-list #chipList [attr.aria-label]="model.label" *ngIf="form.get(model.id).valueChanges | async as chipItems">
		<mat-chip
			*ngFor="let sm of chipItems"
			[selectable]="true"
			[removable]="true"
			(removed)="remove(sm)">
			{{displayFn(sm)}}
			<mat-icon matChipRemove>cancel</mat-icon>
		</mat-chip>
		<input
			placeholder="Select movie"
			#fruitInput
			[formControl]="searchItemsCtrl"
			[matAutocomplete]="auto"
			[matChipInputFor]="chipList"
			[matChipInputSeparatorKeyCodes]="[]"
			[matChipInputAddOnBlur]="false"
			(matChipInputTokenEnd)="add($event)">
	</mat-chip-list>
	<mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn" (optionSelected)="selectItem($event)">
		<ng-container>
			<mat-option *ngFor="let item of filteredItems | async" [value]="item">
				{{displayFn(item)}}
			</mat-option>
		</ng-container>
	</mat-autocomplete>

	<mat-hint>{{model.hint}}</mat-hint>
	<mat-error  *ngIf="form.controls[model.id].invalid">
		{{formErrorService.getErrorMessage(form,model.id)}}
	</mat-error>
</mat-form-field>
</div>
  `,
})
export class RaMultiselectComponent implements OnInit{

	@Input() model: RaMultiselect;

	@Input()
	form: FormGroup;

	filteredItems: Observable<any[]>;

	searchItemsCtrl = new FormControl();

	constructor(public formErrorService: FormErrorService, private http: HttpClient) { }

	ngOnInit(): void {
		if (this.isRemoteSource(this.model.source)) {
			
			const source: RaMultiselectRemoteSource = <RaMultiselectRemoteSource>this.model.source;
			this.filteredItems = this.searchItemsCtrl.valueChanges
				.pipe(
					startWith(''),
					debounceTime(200),
					distinctUntilChanged(),
					map((value: string) => this.mapToHttpParams(value, source)),
					switchMap(query => this.http.get<PagedResult>(source.url, { params: query })),
					map(result => this.pagedResultToItems(result, source))
				);
		}
	}
	
	add(event: MatChipInputEvent): void {
		const input = event.input;

		// Reset the input value
		if (input) {
			input.value = '';
		}
	}

	displayFn(item?: any): string | undefined {
		if (!item) {
			return undefined;
		}

		let result: string = "";

		for (let i = 0; i < this.model.displayColumns.length; i++) {
			if (i) {
				result += "; ";
			}
			result += item[this.model.displayColumns[i]];
		}

		return result;
	}

	remove(item: any): void {
		let items = this.getCurrentSelectedItems();
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);
		}
	}

	selectItem(event: MatAutocompleteSelectedEvent) {
		let items = this.getCurrentSelectedItems();

		if (items) {
			items.push(event.option.value);
		}
	}

	isRemoteSource(source: RaMultiselectSources): source is RaMultiselectRemoteSource {
		return source.kind === "remote";
	}

	private getCurrentSelectedItems(): any[] {
		let value: any[] = this.form.get(this.model.id).value;

		return value;
	}

	private mapToHttpParams(searchText, source: RaMultiselectRemoteSource): HttpParams {

		const queryConfigurator = new QueryParamsConfigurator();
		queryConfigurator.setPageSize(source.maxItems || 50);
		queryConfigurator.setCurrentPage(1);

		source.filterColumns.forEach(column => {
			queryConfigurator.orContains(column, searchText);
		});

		const queryParams = queryConfigurator.toQueryParams();
		return  RaUtils.toHttpParams(queryParams);
	}

	private pagedResultToItems(pagedResult: PagedResult, source: RaMultiselectRemoteSource): any[] {
		let array:any[] = [];
		if (pagedResult.entities && pagedResult.entities.length) {

			pagedResult.entities.forEach(entity => {
				let item = {};
				if (source.remoteMapping) {
					for (let key in source.remoteMapping) {
						item[source.remoteMapping[key]] = entity[key];
					}
				} else {
					item = entity;
				}
				array.push(item);
			});

		}
		return array;
	}
}
