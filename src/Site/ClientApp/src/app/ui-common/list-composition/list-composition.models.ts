import { RaTextBox, RaSelectBox } from "../forms-composition/forms-composition.models";

export interface GridContentOptions {
	canOrder: boolean;
	valueKind:'string'|'decimal'|'percentage';
}

export interface GridControlPanel {
	showAddButton: boolean;
}

export interface RaGridColumn {
	id: string;
	name: string;
	options?: GridContentOptions;
	columns?: RaGridColumn[];
}



export interface RaServerDataGridModel {
	entitiesName: string;
	columns: RaGridColumn[];
	pageSize?: number;
	panel?: GridControlPanel;
	filter: RaGridFilterModel | null;
}

export type FilterControlKinds = RaTextBox | RaSelectBox;


export interface RaGridFilterModel {
	columns: FilterControlKinds[];
}
