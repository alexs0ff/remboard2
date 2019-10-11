import { Component, OnInit,Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, FormArray } from "@angular/forms";
import { KeyValue } from "../../../app.models";
import { RaGridFilterModel } from "../list-composition.models";

@Component({
  selector: 'ra-grid-filter',
  templateUrl: './ra-grid-filter.component.html',
  styleUrls: ['./ra-grid-filter.component.scss']
})
export class RaGridFilterComponent implements OnInit {
	filtersForm: FormGroup;

	logicalOperators: KeyValue<string>[] = [{ key: "and", value: "и" }, { key: "or", value: "или" }];
	columns: KeyValue<string>[] = [{ key: "column1", value: "Название" }, { key: "column2", value: "Тип1" }];
	comparisonOperators: KeyValue<string>[] = [{ key: "equals", value: "Равно" }, { key: "Contains", value: "Содержит" }];

	@Input()
	model: RaGridFilterModel;

	constructor(private formBuilder: FormBuilder) {
		this.filtersForm = this.formBuilder.group({
      filters:this.formBuilder.array([])
    });
  }

	ngOnInit() {

	}

	get filters() {
		return this.filtersForm.get('filters') as FormArray;
	}

	addFilter() {
    this.filters.push(this.formBuilder.control(''));
	}

	clearFilter(i:number) {
		this.filters.removeAt(i);
	}

	startRefreshData() {
    console.log(this.filtersForm.value);
  }
}
