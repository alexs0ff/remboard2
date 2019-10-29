import { FilterableEntityBase } from "./ra-schema-cruds.models";
import { createAction, props } from '@ngrx/store';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";
import { QueryParams } from "../ra-cruds.module";


export class EntitySchemaActions<T extends FilterableEntityBase> {
	constructor(private entityName: string) {
	}

	//RaServerDataGridModel
	updateGridModel = createAction('[' + this.entityName + ' Schema] update data', props<{model: RaServerDataGridModel}>());
}
export const loadGridModelWithQuery = createAction('Load grid schema with query from API', props<{ entitiesName: string, queryParams: QueryParams }>());
