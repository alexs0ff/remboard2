import { Action, ActionReducer } from '@ngrx/store';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";
import { QueryParams } from "../ra-cruds.module";
import { Observable } from "rxjs";
import { RaServerDataGridModel as IRaServerDataGridModel } from "../../../ra-schema/ra-schema-grid.models";


export interface FilterableEntityBase {
	
}


export interface EntitySchemaState {
	model:RaServerDataGridModel
}

export interface IEntitySchemaConfigurator<T extends FilterableEntityBase> {
	entityReducer: ActionReducer<EntitySchemaState, Action>;
	configure(featureName: string, entitiesName: string);
	singleEntityName: string;
}



export interface IEntitySchemaService<T extends FilterableEntityBase> {
	gridModel: Observable<IRaServerDataGridModel>;
	getWithQuery(queryParams: QueryParams);
}

export interface EntitySchemaMetadata {
	[p: string]: IEntitySchemaConfigurator<any>;
}

