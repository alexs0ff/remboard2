import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap, catchError, tap, withLatestFrom,first } from 'rxjs/operators';
import { RaUtils } from "../ra-cruds.utils";
import { Store, select } from "@ngrx/store";
import { EMPTY, of,iif } from 'rxjs';
import { loadGridModelWithQuery, EntitySchemaActions } from "./ra-schema-cruds.actions";
import { EntitySchemaServiceApiFactory, EntitySchemaConfiguratorRegistry, IEntitySchemaApiService, EntitySchemaServiceFactory } from "./ra-schema-cruds.services";
import { IEntitySchemaService } from "../ra-cruds.module";

@Injectable()
export class EntitySchemaEffects {
	loadwithQueryEntities$ = createEffect(() => this.actions$.pipe(
		ofType(loadGridModelWithQuery),
		mergeMap((e) => this.getService(e.entitiesName).gridModel.pipe(first(), map(model => [{action:e,model:model}]))),
		/*mergeMap(([{ action, model}]) => this.getApiService(action.entitiesName).getWithQuery(action.queryParams).pipe(
			map(result => this.getEntityActions(action.entitiesName).updateGridModel({ model: result })),
			catchError((error) => {
				const parsed = RaUtils.parseHttpError(error);
				//return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
				return EMPTY;
			})
		))*/
		mergeMap(([{ action, model }]) => {
			if (!action.force && model) {
				return EMPTY;
			}

			return this.getApiService(action.entitiesName).getWithQuery(action.queryParams).pipe(
				map(result => this.getEntityActions(action.entitiesName).updateGridModel({ model: result })),
				catchError((error) => {
					const parsed = RaUtils.parseHttpError(error);
					//return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
					return EMPTY;
				})
			);
		}))

		/*
		mergeMap(([e, model]) => {
				return this.getApiService(e.entitiesName).getWithQuery(e.queryParams).pipe(
					map(result => this.getEntityActions(e.entitiesName).updateGridModel({ model: result })),
					catchError((error) => {
						const parsed = RaUtils.parseHttpError(error);
						//return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
						return EMPTY;
					})
				);
			}
		)*/

	);



	constructor(private actions$: Actions, private entityServiceApiFactory: EntitySchemaServiceApiFactory, private configuratorRegistry: EntitySchemaConfiguratorRegistry, private entitySchemaServiceFactory: EntitySchemaServiceFactory) {

	}

	private getApiService(entitiesName: string): IEntitySchemaApiService {
		return this.entityServiceApiFactory.getApiService(entitiesName);
	}

	private getService(entitiesName: string): IEntitySchemaService<any> {
		return this.entitySchemaServiceFactory.getService(entitiesName);
	}

	private getEntityActions(entitiesName: string): EntitySchemaActions<any> {
		return this.configuratorRegistry.getEntityActions(entitiesName);
	}
}
