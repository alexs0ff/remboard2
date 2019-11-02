import { createAction, props } from '@ngrx/store';
import { RaEntityEdit } from "../../../ra-schema/ra-schema.module";
import { QueryParams, IEntityBase } from "../ra-cruds.module";


export class EntityEditSchemaActions<T extends IEntityBase> {
	constructor(private entityName: string) {
	}

	updateEditModel = createAction('[' + this.entityName + ' Edit Schema] update data', props<{ model: RaEntityEdit,layouts:string[] }>());
}
export const loadEditModelWithQuery = createAction('Load edit form schema with query from API', props<{ entitiesName: string, queryParams: QueryParams}>());
