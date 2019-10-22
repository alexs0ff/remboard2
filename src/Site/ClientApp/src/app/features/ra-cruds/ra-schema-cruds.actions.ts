import { FilterableEntityBase } from "./ra-schema-cruds.models";
import { createAction, props } from '@ngrx/store';


export class SchemaEntityActions<T extends FilterableEntityBase> {
	constructor(private entityName: string) {
	}

	//RaServerDataGridModel
	//loadEntities = createAction('[' + this.entityName + ' Page] Load Schema', props<{entities: RaServerDataGridModel}>());
}
