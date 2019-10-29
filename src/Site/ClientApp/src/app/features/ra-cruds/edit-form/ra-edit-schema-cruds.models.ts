import { Action, ActionReducer } from '@ngrx/store';
import { QueryParams, IEntityBase } from "../ra-cruds.module";
import { Observable } from "rxjs";
import { RaEntityEdit } from "../../../ra-schema/ra-schema-forms.models";


export interface EntityEditSchemaState {
	model: RaEntityEdit
}

export interface IEntityEditSchemaConfigurator<T extends IEntityBase> {
	entityReducer: ActionReducer<EntityEditSchemaState, Action>;
	configure(featureName: string, entitiesName: string);
	singleEntityName: string;
}



export interface IEntityEditSchemaService<T extends IEntityBase> {
	editModel: Observable<RaEntityEdit>;
	getWithQuery(queryParams: QueryParams);
}

export interface EntityEditSchemaMetadata {
	[p: string]: IEntityEditSchemaConfigurator<any>;
}

