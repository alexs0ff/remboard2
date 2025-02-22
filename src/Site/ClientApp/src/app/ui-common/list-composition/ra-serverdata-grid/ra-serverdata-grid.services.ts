import { Injectable } from '@angular/core';
import { RaGridFlatModel, RaGridColumn,  RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";

@Injectable()
export class GridModelComposer {
	toFlatModel(model: RaServerDataGridModel): RaGridFlatModel {
		let result: RaGridFlatModel = {
			columns: [],
			filter: model.filter,
			headers: [],
			pageSize: model.pageSize,
			panel: model.panel,
			displayedColumns:[]
		};

		this.processHeaders(model.columns, 0, result);

		return result;
	}


	private processHeaders(columns: RaGridColumn[], currentLevel: number, result: RaGridFlatModel): number {
		let sumSpan = 1;
		if (result.headers.length<=currentLevel) {
			result.headers.push([]);
		}
		for (var i = 0; i <columns.length; i++) {
			let levelColumn: RaGridColumn = columns[i];

			let flatColumn = {
				colspan: -1,
				id: levelColumn.id,
				name: levelColumn.name,
				options: levelColumn.options,
				level: currentLevel
			};
			result.columns.push(flatColumn);

			if (levelColumn.options) {
				result.displayedColumns.push(levelColumn.id);
			}

			result.headers[currentLevel].push(levelColumn.id);

			if (levelColumn.columns && levelColumn.columns.length) {
				sumSpan += this.processHeaders(levelColumn.columns, currentLevel + 1, result);
			}

			flatColumn.colspan = sumSpan;

		}

		return sumSpan;
	}

	public static convertToFlat(gridModelComposer: GridModelComposer, model: RaServerDataGridModel): RaGridFlatModel {
		if (!model) {
			return null;
		}

		let converted = gridModelComposer.toFlatModel(model);
		if (model.panel) {
			converted.displayedColumns.splice(0, 0, "gridControlPanel");
			converted.headers[0].splice(0, 0, "gridControlPanel");

			for (let i = 1; i < converted.headers.length; i++) {
				converted.headers[i].splice(0, 0, "gridHiddenColumn");
			}
		}
		return converted;
	}

}
