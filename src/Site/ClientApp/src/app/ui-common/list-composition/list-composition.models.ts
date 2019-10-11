import { Dictionary } from "../../app.models";
import { RaTextBox, RaSelectBox } from "../forms-composition/forms-composition.models";

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

export type FilterControlKinds = RaTextBox | RaSelectBox;


export interface RaGridFilterModel {
	columns: FilterControlKinds[];
}
