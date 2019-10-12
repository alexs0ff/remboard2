import { Injectable } from '@angular/core';
import { KeyValue } from "../../../app.models";
import { FilterControlKinds, RaGridFilterModel } from "../list-composition.models";
import { RaTextBox, RaSelectBox } from "../../forms-composition/forms-composition.models";
import { ComparisonOperators } from "../../ra-filter.models";


const ControlsComparisonOperatorsMap = {
	'selectbox': {
		'number': [ComparisonOperators.Equals],
		'string': [ComparisonOperators.Equals],
	},
	'textbox': {
		'number': [ComparisonOperators.Equals, ComparisonOperators.LessThan, ComparisonOperators.GreaterThan],
		'string': [ComparisonOperators.Equals, ComparisonOperators.Contains],
	}
};

const TitleComparisonOperatorsMap = {
	"equals": "Равно",
	"lessthan":"Меньше чем",
	"greaterthan":"Больше чем",
	"contains":"Содержит",
}


@Injectable()
export class GridFilterCompositionService {

	comparisonOperators: KeyValue<string>[] = [
		{ key: ComparisonOperators.Equals, value: "Равно" },
		{ key: ComparisonOperators.Equals, value: "Содержит" }
	];

	public toComparisonOperators(model: RaGridFilterModel, columnId: string): KeyValue<string>[] {
		let result: KeyValue<string>[] = [];
		let column: FilterControlKinds = this.getControlById(model, columnId);
		let columnKind = ControlsComparisonOperatorsMap[column.kind];

		let comparationOperators = columnKind[column.valueKind];

		for (var i = 0; i < comparationOperators.length; i++) {

			let comparationOperator = comparationOperators[i];
			let title = TitleComparisonOperatorsMap[comparationOperator];

			result.push({ key: comparationOperator, value: title });	
		}

		return result;
	}

	public toColumns(model: RaGridFilterModel): KeyValue<string>[] {
		let result: KeyValue<string>[] = [];

		for (let i = 0; i < model.columns.length; i++) {
			let column: FilterControlKinds = model.columns[i];
			if (this.isFilterTextControl(column)) {
				result.push({ key: column.id, value: column.label });
			} else if (this.isFilterSelectControl(column)) {
				result.push({ key: column.id, value: column.label });
			}
		}
		return result;
	}

	public getControlById(model: RaGridFilterModel, columnId: string): FilterControlKinds {
		let result = null;
		for (let i = 0; i < model.columns.length; i++) {
			let column: FilterControlKinds = model.columns[i];
			if (column.id === columnId) {
				result = { ...column, id: "value" }
			}

		}
		return result;
	}

	private isFilterTextControl(column: FilterControlKinds): column is RaTextBox {
		return column.kind === "textbox";
	}

	private isFilterSelectControl(column: FilterControlKinds): column is RaSelectBox {
		return column.kind === "selectbox";
	}
}
