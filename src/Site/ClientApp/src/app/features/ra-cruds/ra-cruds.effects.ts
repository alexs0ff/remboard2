import { Injectable } from '@angular/core';
import { IEntityBase, EntityResponse } from "./ra-cruds.models";
import { map, mergeMap, catchError, tap  } from 'rxjs/operators';
import { Actions, Effect, ofType, createEffect } from '@ngrx/effects';
import { loadAllEntities } from "./ra-cruds.actions";
import { EMPTY,of} from 'rxjs';
import { EntityServiceApiFactory } from "./ra-cruds.services";

@Injectable()
export class RaCrudsEntityEffects {
  loadEntities$ = createEffect(() => this.actions$.pipe(
    ofType(loadAllEntities),
    mergeMap((e) => this.entityServiceApiFactory.getApiService(e.entitiesName).getAll().pipe(
      map(pagedResult => this.entityServiceApiFactory.getEntityActions(e.entitiesName).loadEntities({ entities: pagedResult.entities, totalCount:pagedResult.count })),
      catchError((error) => {
        const parsed = this.parseError(error);
        return of(this.entityServiceApiFactory.getEntityActions(e.entitiesName).setApiError({error:parsed}));
      })
    ))));
  constructor(private actions$: Actions, private entityServiceApiFactory: EntityServiceApiFactory) {
    
  }

  private parseError(error: any): EntityResponse {
    return { validationErrors: [], message:"untyped message"}
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
