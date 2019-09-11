export interface RaGridColumn {
  id:string;
  name: string;
  canOrder:boolean;
}
export interface RaServerDataGridModel {
  entitiesName: string;
  columns: RaGridColumn[];
  pageSize:number|null;
}
