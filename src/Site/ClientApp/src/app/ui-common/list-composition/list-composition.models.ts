import { Dictionary } from "../../app.models";

export interface RaGridColumn {
  id:string;
  name: string;
  canOrder:boolean;
}
export interface RaServerDataGridModel {
  entitiesName: string;
  columns: RaGridColumn[];
  pageSize: number | null;
	showAddButton: boolean;
	filter: RaGridFilterModel|null;
}


export interface FilterTextControl {
	kind: 'text';
	id: string;
  title: string;
}

export interface FilterSelectboxControl {
  kind: 'selectbox';
}

type FilterControlKinds = FilterTextControl | FilterSelectboxControl;


export interface RaGridFilterModel {
	columns: FilterControlKinds[];
}
