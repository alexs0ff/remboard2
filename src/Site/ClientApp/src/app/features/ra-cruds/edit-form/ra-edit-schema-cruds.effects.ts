import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap, catchError, tap, first } from 'rxjs/operators';
import { RaUtils } from "../ra-cruds.utils";

import { EMPTY, of } from 'rxjs';
import { loadEditModelWithQuery, EntityEditSchemaActions } from "./ra-edit-schema-cruds.actions";
import { EntityEditSchemaServiceApiFactory, EntityEditSchemaConfiguratorRegistry, IEntityEditSchemaApiService,
	EntityEditSchemaServiceFactory
} from "./ra-edit-schema-cruds.services";
import { IEntityEditSchemaService } from "./ra-edit-schema-cruds.models";


@Injectable()
export class EntityEditSchemaEffects {
	loadwithQueryEntities$ = createEffect(() => this.actions$.pipe(
		ofType(loadEditModelWithQuery),
		mergeMap((e) => this.getService(e.entitiesName).editModel.pipe(first(), map(model => [{ action: e, model: model }]))),
		mergeMap(([{ action, model }]) => {
			if (!action.force && model) {
				return EMPTY;
			}
			return this.getApiService(action.entitiesName).getWithQuery(action.queryParams).pipe(
				map(result => this.getEntityActions(action.entitiesName).updateEditModel({ model: result.entityEdit, layouts: result.displayedLayoutIds })),
				catchError((error) => {
					const parsed = RaUtils.parseHttpError(error);
					//return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
					return EMPTY;
				})
			);
		}))
		
	);

	constructor(private actions$: Actions, private entityEditSchemaServiceApiFactory: EntityEditSchemaServiceApiFactory, private configuratorRegistry: EntityEditSchemaConfiguratorRegistry, private entityEditSchemaServiceFactory: EntityEditSchemaServiceFactory) {

	}

	private getApiService(entitiesName: string): IEntityEditSchemaApiService {
		return this.entityEditSchemaServiceApiFactory.getApiService(entitiesName);
	}

	private getEntityActions(entitiesName: string): EntityEditSchemaActions<any> {
		return this.configuratorRegistry.getEntityActions(entitiesName);
	}

	private getService(entitiesName: string): IEntityEditSchemaService<any> {
		return this.entityEditSchemaServiceFactory.getService(entitiesName);
	}
}
