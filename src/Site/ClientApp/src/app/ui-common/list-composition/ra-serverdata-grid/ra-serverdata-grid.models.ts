import { GridContentOptions, RaGridFilterModel, GridControlPanel } from "../list-composition.models";

export interface RaGridInternalColumn {
	id: string;
	name: string;
	colspan: number,
	options?: GridContentOptions;
	level:number;
}



export interface RaGridFlatModel {
	displayedColumns: Array<string>;
	headers: Array<Array<string>>;
	columns: RaGridInternalColumn[];
	pageSize?: number;
	panel?: GridControlPanel;
	filter: RaGridFilterModel | null;
}
