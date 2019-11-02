import { Component, OnInit, Input, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { MatAutocompleteTrigger } from '@angular/material';
import { FormGroup } from "@angular/forms";
import { Observable, Subject } from 'rxjs';
import { map, startWith, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { MatChipInputEvent } from '@angular/material';

import { FormControl } from '@angular/forms';
import { RaMultiselect, RaMultiselectRemoteSource, RaMultiselectSources } from "../../../ra-schema/ra-schema.module";
import { EntityServiceApiFactory, QueryParams, QueryParamsConfigurator, PagedResult } from "../../../features/ra-cruds/ra-cruds.module";
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { HttpClient } from '@angular/common/http';
import { RaUtils } from "../../../features/ra-cruds/ra-cruds.utils";
import { HttpParams } from "@angular/common/http/http";
import { FormErrorService } from "../form-error-service";
import { HiddenErrorStateMatcher } from "../forms-composition-service";


@Component({
	selector: 'ra-multiselect',
	template: `
<div [formGroup]="form">
  <mat-form-field class="ra-mat-field">
	<input matInput class="invisible" name="{{model.id}}" [formControlName]="model.id" [errorStateMatcher]="matcher"/>
    <mat-label>{{model.label}}</mat-label>
	<mat-chip-list #chipList [attr.aria-label]="model.label">
		<mat-chip
			*ngFor="let sm of form.get(model.id).valueChanges | async"
			[selectable]="true"
			[removable]="true"
			(removed)="remove(sm)">
			{{displayFn(sm)}}
			<mat-icon matChipRemove>cancel</mat-icon>
		</mat-chip>
		<input	#filterInput placeholder="{{model.label}}"
			(focus)="onFilterFocus()"
			[formControl]="searchItemsCtrl"
			[matAutocomplete]="auto"
			[matChipInputFor]="chipList"
			[matChipInputSeparatorKeyCodes]="[]"
			[matChipInputAddOnBlur]="false"
			(matChipInputTokenEnd)="add($event)">
	</mat-chip-list>
	<mat-autocomplete #auto="matAutocomplete" [displayWith]="displayFn" (optionSelected)="selectItem($event)">
		<mat-option *ngIf="isLoading">Loading...</mat-option>
		<ng-container *ngIf="!isLoading">
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
	styles: [`
.invisible{
	display: block;
	visibility: hidden;
	height: 0;
	width: 0;
}
`]
})
export class RaMultiselectComponent implements OnInit{
	@Input() model: RaMultiselect;

	@Input()
	form: FormGroup;

	@ViewChild('filterInput', {static:false})
	filterInput: ElementRef;

	@ViewChild('filterInput', { static: false, read: MatAutocompleteTrigger }) autoCompleteTrigger: MatAutocompleteTrigger;
	

	filteredItems: Observable<any[]>;

	searchItemsCtrl = new FormControl();
	isLoading = false;

	matcher: HiddenErrorStateMatcher;

	constructor(public formErrorService: FormErrorService, private http: HttpClient) {
		this.matcher = new HiddenErrorStateMatcher(this.searchItemsCtrl);
	}

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
			this.filterInput.nativeElement.value = '';
		}
	}

	displayFn(item?: any): string | undefined {
		if (!item) {
			return undefined;
		}

		let result: string = "";

		if (!this.model) {
			return "WRONG STATE";
		}

		for (let i = 0; i < this.model.displayColumns.length; i++) {
			if (i) {
				result += "; ";
			}
			result += item[this.model.displayColumns[i]];
		}

		return result;
	}

	onFilterFocus() {
		this.autoCompleteTrigger._onChange('');
		this.autoCompleteTrigger.openPanel();
	}

	remove(item: any): void {
		let items = this.getCurrentSelectedItems();
		const index = items.indexOf(item);

		if (index >= 0) {
			this.setCurrentSelectedItems(items);
			items = items.slice();
			items.splice(index, 1);
			this.setCurrentSelectedItems(items);
		}
	}

	selectItem(event: MatAutocompleteSelectedEvent) {
		let items = this.getCurrentSelectedItems();
		
		if (items) {
			items = items.slice();
		} else {
			items = [];
		}

		items.push(event.option.value);

		this.setCurrentSelectedItems(items);
		this.filterInput.nativeElement.value = '';
	}

	isRemoteSource(source: RaMultiselectSources): source is RaMultiselectRemoteSource {
		return source.kind === "remote";
	}

	private getCurrentSelectedItems(): any[] {
		let value: any[] = this.form.get(this.model.id).value;

		return value;
	}

	private setCurrentSelectedItems(newItems: any[]){
		this.form.get(this.model.id).setValue(newItems);
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
