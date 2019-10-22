import { RaSelectBox, RaTextBox } from "./ra-schema-forms.models";

export interface GridContentOptions {
	canOrder: boolean;
	valueKind: 'string' | 'decimal' | 'percentage';
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


export interface RaGridInternalColumn {
	id: string;
	name: string;
	colspan: number,
	options?: GridContentOptions;
	level: number;
}



export interface RaGridFlatModel {
	displayedColumns: Array<string>;
	headers: Array<Array<string>>;
	columns: RaGridInternalColumn[];
	pageSize?: number;
	panel?: GridControlPanel;
	filter: RaGridFilterModel | null;
}
