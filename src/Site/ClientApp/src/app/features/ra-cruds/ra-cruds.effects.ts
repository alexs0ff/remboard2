import { Injectable } from '@angular/core';
import { IEntityBase, EntityResponse } from "./ra-cruds.models";
import { map, mergeMap, catchError, tap  } from 'rxjs/operators';
import { Actions, Effect, ofType, createEffect } from '@ngrx/effects';
import { loadAllEntities, EntityActions, createEntity, updateEntity, deleteEntity, loadWithQueryEntities, loadByIdEntity } from "./ra-cruds.actions";
import { EMPTY,of} from 'rxjs';
import { EntityServiceApiFactory, IEntityApiService, ConfiguratorRegistry } from "./ra-cruds.services";
import { RaUtils } from "./ra-cruds.utils";
import { Update} from '@ngrx/entity';

@Injectable()
export class RaCrudsEntityEffects {
  loadEntities$ = createEffect(() => this.actions$.pipe(
    ofType(loadAllEntities),
    mergeMap((e) => this.getApiService(e.entitiesName).getAll().pipe(
      map(pagedResult => this.getEntityActions(e.entitiesName).loadEntities({ entities: pagedResult.entities, totalCount:pagedResult.count })),
      catchError((error) => {
        const parsed = RaUtils.parseHttpError(error);
        return of(this.getEntityActions(e.entitiesName).setApiError({error:parsed}));
      })
    ))));

  loadwithQueryEntities$ = createEffect(() => this.actions$.pipe(
    ofType(loadWithQueryEntities),
    mergeMap((e) => this.getApiService(e.entitiesName).getWithQuery(e.queryParams).pipe(
      map(pagedResult => this.getEntityActions(e.entitiesName).loadEntities({ entities: pagedResult.entities, totalCount: pagedResult.count })),
      catchError((error) => {
        const parsed = RaUtils.parseHttpError(error);
        return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
      })
    ))));

  createEntity$ = createEffect(() => this.actions$.pipe(
    ofType(createEntity),
    mergeMap((e) => this.getApiService(e.entitiesName).add(e.entity).pipe(
		map(result => this.getEntityActions(e.entitiesName).addEntity({ entity: result, correlationId: e.correlationId})),
      catchError((error) => {
        const parsed = RaUtils.parseHttpError(error);
        return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
      })
    ))));

  updateEntity$ = createEffect(() => this.actions$.pipe(
    ofType(updateEntity),
    mergeMap((e) => this.getApiService(e.entitiesName).update(e.id, e.entity).pipe(
      map(entity=>this.mapEntityToUpdatable(entity)),
      map(result => this.getEntityActions(e.entitiesName).updateEntity({ entity: result })),
      catchError((error) => {
        const parsed = RaUtils.parseHttpError(error);
        return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
      })
    ))));
  
  deleteEntity$ = createEffect(() => this.actions$.pipe(
    ofType(deleteEntity),
    mergeMap((e) => this.getApiService(e.entitiesName).delete(e.id).pipe(
      map(result => this.getEntityActions(e.entitiesName).deleteEntity({ id: e.id })),
      catchError((error) => {
        const parsed = RaUtils.parseHttpError(error);
        return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
      })
    ))));

  loadByIdEntity$ = createEffect(() => this.actions$.pipe(
    ofType(loadByIdEntity),
    mergeMap((e) => this.getApiService(e.entitiesName).getById(e.id).pipe(
      map(result => this.getEntityActions(e.entitiesName).upsertEntityAndSetCurrentId({entity:result})),
      catchError((error) => {
        const parsed = RaUtils.parseHttpError(error);
        return of(this.getEntityActions(e.entitiesName).setApiError({ error: parsed }));
      })
    ))));


  private mapEntityToUpdatable<T extends IEntityBase>(entity: T): Update<T> {
    return { id: entity.id, changes: entity}
  }

  private getApiService(entitiesName:string): IEntityApiService {
    return this.entityServiceApiFactory.getApiService(entitiesName);
  }

  private getEntityActions(entitiesName: string): EntityActions<any> {
    return this.configuratorRegistry.getEntityActions(entitiesName);
  }

  constructor(private actions$: Actions, private entityServiceApiFactory: EntityServiceApiFactory, private configuratorRegistry: ConfiguratorRegistry) {
    
  }

}

@Injectable()
export class RaCrudsEntityEffects2 {
  constructor(private actions$: Actions) { }
  logActions$ = createEffect(() =>
    this.actions$.pipe(
      tap(action => console.log(action))
    ), { dispatch: false });

}
