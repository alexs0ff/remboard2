import { Injectable } from '@angular/core'
import { Action, createAction, props, ActionReducer, on, createReducer } from '@ngrx/store';
import { EntityState, createEntityAdapter } from '@ngrx/entity';
import { IEntityBase, CrudAdapter, IState, IEntityService, ICrudEntityConfigurator } from "./ra-cruds.models"
import { Update, Predicate, EntityMap } from '@ngrx/entity';


class EntityActions<T extends IEntityBase> {
  private entityName: string;

  constructor(entityName: string) {
    this.entityName = entityName;
  }

  loadEntities = createAction('[' + this.entityName + ' Page] Load Entities', props<{ entities: T[] }>());
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
  setCurrentEntity = createAction('[' + this.entityName + ' Page] Set current Entity', props<{ id: number }>());
}


export class CrudEntityConfigurator<T extends IEntityBase> implements ICrudEntityConfigurator<T> {

  private adapter: CrudAdapter<T>;
  private initialState: IState<T>;

  private EntityActions: EntityActions<T>;

  entityReducer: ActionReducer<EntityState<T>, Action>;

  public configure(entityName: string)  {
    this.adapter = createEntityAdapter<T>();
    this.initialState = this.adapter.getInitialState({
      selectedId: null,
      totalCount: 0
    });

    this.EntityActions = new EntityActions<T>(entityName);

    this.entityReducer = createReducer(
      this.initialState,
      on(this.EntityActions.addEntity, (state, { entity }) => this.adapter.addOne(entity, state)),
      on(this.EntityActions.upsertEntity, (state, { entity }) => {
        return this.adapter.upsertOne(entity, state);
      }),
      on(this.EntityActions.addEntities, (state, { entities }) => {
        return this.adapter.addMany(entities, state);
      }),
      on(this.EntityActions.upsertEntities, (state, { entities }) => {
        return this.adapter.upsertMany(entities, state);
      }),
      on(this.EntityActions.updateEntity, (state, { entity }) => {
        return this.adapter.updateOne(entity, state);
      }),
      on(this.EntityActions.updateEntities, (state, { entities }) => {
        return this.adapter.updateMany(entities, state);
      }),
      on(this.EntityActions.mapEntities, (state, { entityMap }) => {
        return this.adapter.map(entityMap, state);
      }),
      on(this.EntityActions.deleteEntity, (state, { id }) => {
        return this.adapter.removeOne(id, state);
      }),
      on(this.EntityActions.deleteEntities, (state, { ids }) => {
        return this.adapter.removeMany(ids, state);
      }),
      on(this.EntityActions.deleteEntitiesByPredicate, (state, { predicate }) => {
        return this.adapter.removeMany(predicate, state);
      }),
      on(this.EntityActions.loadEntities, (state, { entities }) => {
        return this.adapter.addAll(entities, state);
      }),
      on(this.EntityActions.clearEntities, (state: IState<T>) => {
        return this.adapter.removeAll({ ...state, selectedEntityId: null });
      }),
      on(this.EntityActions.setCurrentEntity, (state: IState<T>, { id }) => {
        return { ...state, selectedEntityId: id };
      })
    );
  }
}



@Injectable()
export class ConfiguratorRegistry {
  private configurators: { [key: string]: ICrudEntityConfigurator<any> } = {};

  public add(entityName: string, configurator: ICrudEntityConfigurator<any>) {
    this.configurators[entityName] = configurator;
  }

  public getCrudEntityConfigurator<T extends IEntityBase>(entityName: string): ICrudEntityConfigurator<T> {
    return this.configurators[entityName];
  }

}


export class EntityService<T extends IEntityBase> implements IEntityService<T> {
  constructor(private configurator: ICrudEntityConfigurator<T>) { }
}


@Injectable()
export class EntityServiceFabric {
  constructor(private registry: ConfiguratorRegistry) { }

  public getService<T extends IEntityBase>(entityName: string): IEntityService<T> {
    const configurator = this.registry.getCrudEntityConfigurator<T>(entityName);
    return new EntityService<T>(configurator);
  }
}
