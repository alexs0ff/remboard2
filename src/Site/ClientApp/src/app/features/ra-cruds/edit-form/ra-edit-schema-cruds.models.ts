import { Action, ActionReducer } from '@ngrx/store';
import { QueryParams, IEntityBase } from "../ra-cruds.module";
import { Observable } from "rxjs";
import { RaEntityEdit } from "../../../ra-schema/ra-schema-forms.models";


export interface EntityEditSchemaState {
	model: RaEntityEdit,
	layouts:string[],
}

export interface IEntityEditSchemaConfigurator<T extends IEntityBase> {
	entityReducer: ActionReducer<EntityEditSchemaState, Action>;
	configure(featureName: string, entitiesName: string);
	singleEntityName: string;
}



export interface IEntityEditSchemaService<T extends IEntityBase> {
	editModel: Observable<RaEntityEdit>;
	layoutIds: Observable<string[]>;
	getEditFormWithQuery(queryParams: QueryParams);
	getCreateFormWithQuery(queryParams: QueryParams);
	updateModel(model: RaEntityEdit, layouts: string[]);
}

export interface EntityEditSchemaMetadata {
	[p: string]: IEntityEditSchemaConfigurator<any>;
}

