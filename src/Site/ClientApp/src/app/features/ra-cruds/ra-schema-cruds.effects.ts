import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap, catchError, tap } from 'rxjs/operators';
import { RaUtils } from "./ra-cruds.utils";
import { Update } from '@ngrx/entity';
import { EMPTY, of } from 'rxjs';
import { loadGridModelWithQuery, EntitySchemaActions } from "./ra-schema-cruds.actions";
import { EntitySchemaServiceApiFactory, EntitySchemaConfiguratorRegistry, IEntitySchemaApiService } from "./ra-schema-cruds.services";

@Injectable()
export class EntitySchemaEffects {
	loadwithQueryEntities$ = createEffect(() => this.actions$.pipe(
		ofType(loadGridModelWithQuery),
		mergeMap((e) => this.getApiService(e.entitiesName).getWithQuery(e.queryParams).pipe(
			map(result => this.getEntityActions(e.entitiesName).updateGridModel({ model: result})),
			catchError((error) => {
				const parsed = RaUtils.parseHttpError(error);
				//return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
				return EMPTY;
			})
		))));



	constructor(private actions$: Actions, private entityServiceApiFactory: EntitySchemaServiceApiFactory, private configuratorRegistry: EntitySchemaConfiguratorRegistry) {

	}

	private getApiService(entitiesName: string): IEntitySchemaApiService {
		return this.entityServiceApiFactory.getApiService(entitiesName);
	}

	private getEntityActions(entitiesName: string): EntitySchemaActions<any> {
		return this.configuratorRegistry.getEntityActions(entitiesName);
	}
}
