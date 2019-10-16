import { Injectable } from '@angular/core';
import { RaGridFilterModel, RaServerDataGridModel, RaGridColumn } from "../list-composition.models";
import { RaGridFlatModel, RaGridInternalColumn, RaGridInternalColumn as IRaGridInternalColumn } from "./ra-serverdata-grid.models";

@Injectable()
export class GridModelComposer {
	toFlatModel(model: RaServerDataGridModel): RaGridFlatModel {
		let result: RaGridFlatModel = {
			columns: [],
			filter: model.filter,
			headers: [],
			pageSize: model.pageSize,
			showAddButton: model.showAddButton,
			displayedColumns:[]
		};

		this.processHeaders(model.columns, 0, result);
		

		return result;
	}


	private processHeaders(columns: RaGridColumn[], currentLevel: number, result: RaGridFlatModel): number {
		let maxLevel = currentLevel;
		if (result.headers.length<=currentLevel) {
			result.headers.push([]);
		}
		for (var i = 0; i <columns.length; i++) {
			let levelColumn: RaGridColumn = columns[i];

			result.columns.push({
				colspan: -1,
				id: levelColumn.id,
				name: levelColumn.name,
				options: levelColumn.options,
				level: currentLevel
			});

			if (levelColumn.options) {
				result.displayedColumns.push(levelColumn.id);
			}

			result.headers[currentLevel].push(levelColumn.id);

			if (levelColumn.columns && levelColumn.columns.length) {
				maxLevel = this.processHeaders(levelColumn.columns, currentLevel + 1, result);
			}

		}

		return maxLevel;
	}

}
