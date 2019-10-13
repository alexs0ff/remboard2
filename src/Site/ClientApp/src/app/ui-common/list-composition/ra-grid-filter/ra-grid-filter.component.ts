import { Component, OnInit,Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, FormArray, Validators } from "@angular/forms";
import { KeyValue } from "../../../app.models";
import { RaGridFilterModel, FilterControlKinds } from "../list-composition.models";
import { FormErrorService } from "../../forms-composition/form-error-service";
import { GridFilterCompositionService } from "./ra-grid-filter.services";
import { MatSelectChange } from '@angular/material/select';

@Component({
	selector: 'ra-grid-filter',
	templateUrl: './ra-grid-filter.component.html',
	styleUrls: ['./ra-grid-filter.component.scss'],
	providers: [GridFilterCompositionService]
})
export class RaGridFilterComponent implements OnInit {
	filtersForm: FormGroup;

	logicalOperators: KeyValue<string>[] = [{ key: "and", value: "и" }, { key: "or", value: "или" }];
	columns: KeyValue<string>[];


	@Input()
	model: RaGridFilterModel;

	comparisonOperators = [];

	constructor(private formBuilder: FormBuilder, private filterCompositionService: GridFilterCompositionService) {
		this.filtersForm = this.formBuilder.group({
			filters: this.formBuilder.array([])
		});
	}

	ngOnInit() {
		this.columns = this.filterCompositionService.toColumns(this.model);
	}

	get filters(): FormArray {
		return this.filtersForm.get('filters') as FormArray;
	}

	private createItem(): FormGroup {
		return this.formBuilder.group({
			"logicalOperator": this.formBuilder.control('', Validators.required),
			"column": this.formBuilder.control('', Validators.required),
			"comparison": this.formBuilder.control('', Validators.required),
			"value": this.formBuilder.control('', Validators.required)
		});
	}

	columnSelectionChange(event: MatSelectChange, index: number) {
		let columnId = event.value;
		this.comparisonOperators[index] = this.filterCompositionService.toComparisonOperators(this.model, columnId);;
	}
	
	getValueModel(index: number): FilterControlKinds {
		if (this.filters.value.length < index) {
			return null;
		}

		let columnId = this.filters.value[index].column;

		return this.filterCompositionService.getControlById(this.model, columnId);
	}

	getFormGroupByIndex(index: number): FormGroup {
		let result = this.filters.controls[index];
		return result as FormGroup;
	}

	addFilter() {
		this.filters.push(this.createItem());
		this.comparisonOperators.push(null);
	}

	clearFilter(i: number) {
		this.filters.removeAt(i);
		this.comparisonOperators.splice(i, 1);
	}

	startRefreshData() {
		this.filtersForm.markAllAsTouched();
		console.log(this.filtersForm.value);
	}
}
