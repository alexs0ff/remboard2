import { Injectable } from '@angular/core';
import { IEntityBase, EntityResponse } from "./ra-cruds.models";
import { createAction, props} from '@ngrx/store';
import { Update, Predicate, EntityMap } from '@ngrx/entity';

export class EntityActions<T extends IEntityBase> {

  constructor(private entityName: string) {
  }

  loadEntities = createAction('[' + this.entityName + ' Page] Load Entities', props<{ entities: T[],totalCount:number }>());
  addEntity = createAction('[' + this.entityName + ' Page] Add Entity', props<{ entity: T }>());

  upsertEntity = createAction('[' + this.entityName + ' Page] Upsert Entity', props<{ entity: T }>());
  addEntities = createAction('[' + this.entityName + ' Page] Add Entities', props<{ entities: T[] }>());
  upsertEntities = createAction('[' + this.entityName + ' Page] Upsert Entities', props<{ entities: T[] }>());
  updateEntity = createAction('[' + this.entityName + ' Page] Update Entity', props<{ entity: Update<T> }>());
  updateEntities = createAction('[' + this.entityName + ' Page] Update Entities', props<{ entities: Update<T>[] }>());
  mapEntities = createAction('[' + this.entityName + ' Page] Map Entities', props<{ entityMap: EntityMap<T> }>());
  deleteEntity = createAction('[' + this.entityName + ' Page] Delete Entity', props<{ id: string }>());
  deleteEntities = createAction('[' + this.entityName + ' Page] Delete Entities', props<{ ids: string[] }>());
  deleteEntitiesByPredicate = createAction('[' + this.entityName + ' Page] Delete Entities By Predicate', props<{ predicate: Predicate<T> }>());
  clearEntities = createAction('[' + this.entityName + ' Page] Clear Entities');
  setCurrentEntity = createAction('[' + this.entityName + ' Page] Set current Entity', props<{ id: string }>());
  setTotalCount = createAction('[' + this.entityName + ' Page] Set total count', props<{ totalCount: number }>());
  setApiError = createAction('[' + this.entityName + ' Page] Set Error', props<{ error: EntityResponse }>());
  startApiFetch = createAction('[' + this.entityName + ' Page] Start fetch from API');
}

export const loadAllEntities = createAction('Load entities from API', props<{entitiesName:string}>());
