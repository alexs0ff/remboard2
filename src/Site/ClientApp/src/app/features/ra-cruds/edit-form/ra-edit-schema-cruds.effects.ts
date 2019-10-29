import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap, catchError, tap } from 'rxjs/operators';
import { RaUtils } from "../ra-cruds.utils";

import { EMPTY, of } from 'rxjs';
import { loadEditModelWithQuery, EntityEditSchemaActions } from "./ra-edit-schema-cruds.actions";
import { EntityEditSchemaServiceApiFactory, EntityEditSchemaConfiguratorRegistry, IEntityEditSchemaApiService } from "./ra-edit-schema-cruds.services";

@Injectable()
export class EntityEditSchemaEffects {
	loadwithQueryEntities$ = createEffect(() => this.actions$.pipe(
		ofType(loadEditModelWithQuery),
		mergeMap((e) => this.getApiService(e.entitiesName).getWithQuery(e.queryParams).pipe(
			map(result => this.getEntityActions(e.entitiesName).updateGridModel({ model: result})),
			catchError((error) => {
				const parsed = RaUtils.parseHttpError(error);
				//return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
				return EMPTY;
			})
		))));

	constructor(private actions$: Actions, private entityEditSchemaServiceApiFactory: EntityEditSchemaServiceApiFactory, private configuratorRegistry: EntityEditSchemaConfiguratorRegistry) {

	}

	private getApiService(entitiesName: string): IEntityEditSchemaApiService {
		return this.entityEditSchemaServiceApiFactory.getApiService(entitiesName);
	}

	private getEntityActions(entitiesName: string): EntityEditSchemaActions<any> {
		return this.configuratorRegistry.getEntityActions(entitiesName);
	}
}
