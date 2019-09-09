import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Action, createAction, props, ActionReducer, on, createReducer, createSelector, MemoizedSelector } from '@ngrx/store';
import { EntityState, createEntityAdapter } from '@ngrx/entity';
import { IEntityBase, CrudAdapter, IState, IEntityService, ICrudEntityConfigurator } from "./ra-cruds.models"
import { Update, Predicate, EntityMap } from '@ngrx/entity';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';


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
  setCurrentEntity = createAction('[' + this.entityName + ' Page] Set current Entity', props<{ id: string }>());
  setTotalCount = createAction('[' + this.entityName + ' Page] Set total count', props<{ totalCount: number }>());
}

class EntitySelectors<T extends IEntityBase> {
  selectAll: MemoizedSelector<any,T[]>;
  /*private selectIds: (state: IState<T>) => string[] | number[];
  private selectEntities: (state: IState<T>) => { [id: string]: T | undefined;};
  private selectAll: (state: IState<T>) => T[];
  private selectTotal: (state: IState<T>) => number;*/

  constructor(featureName: string,private entityName: string, adapter: CrudAdapter<T>) {
    const { selectIds, selectEntities, selectAll, selectTotal, } = adapter.getSelectors();
    /*this.selectAll = selectAll;
    this.selectIds = selectIds;
    this.selectEntities = selectEntities;
    this.selectTotal = selectTotal;*/

    const getAppState = (state: any) => state[featureName];

    const getModuleStateAny = (state: any) => getAppState(state)[entityName];
    const getModuleState = (state: any) => <IState<T>>getModuleStateAny(state);


    this.selectAll = createSelector(getModuleState, selectAll);
  }


}


export class CrudEntityConfigurator<T extends IEntityBase> implements ICrudEntityConfigurator<T> {

  private adapter: CrudAdapter<T>;
  private initialState: IState<T>;

  entityActions: EntityActions<T>;
  entitySelectors: EntitySelectors<T>;

  entityReducer: ActionReducer<EntityState<T>, Action>;

  public configure(featureName: string,entityName: string)  {
    this.adapter = createEntityAdapter<T>();
    this.initialState = this.adapter.getInitialState({
      selectedId: null,
      totalCount: 0
    });

    this.entityActions = new EntityActions<T>(entityName);

    this.entitySelectors = new EntitySelectors<T>(featureName, entityName,this.adapter);

    this.entityReducer = createReducer(
      this.initialState,
      on(this.entityActions.addEntity, (state, { entity }) => this.adapter.addOne(entity, state)),
      on(this.entityActions.upsertEntity, (state, { entity }) => {
        return this.adapter.upsertOne(entity, state);
      }),
      on(this.entityActions.addEntities, (state, { entities }) => {
        return this.adapter.addMany(entities, state);
      }),
      on(this.entityActions.upsertEntities, (state, { entities }) => {
        return this.adapter.upsertMany(entities, state);
      }),
      on(this.entityActions.updateEntity, (state, { entity }) => {
        return this.adapter.updateOne(entity, state);
      }),
      on(this.entityActions.updateEntities, (state, { entities }) => {
        return this.adapter.updateMany(entities, state);
      }),
      on(this.entityActions.mapEntities, (state, { entityMap }) => {
        return this.adapter.map(entityMap, state);
      }),
      on(this.entityActions.deleteEntity, (state, { id }) => {
        return this.adapter.removeOne(id, state);
      }),
      on(this.entityActions.deleteEntities, (state, { ids }) => {
        return this.adapter.removeMany(ids, state);
      }),
      on(this.entityActions.deleteEntitiesByPredicate, (state, { predicate }) => {
        return this.adapter.removeMany(predicate, state);
      }),
      on(this.entityActions.loadEntities, (state, { entities }) => {
        return this.adapter.addAll(entities, state);
      }),
      on(this.entityActions.clearEntities, (state: IState<T>) => {
        return this.adapter.removeAll({ ...state, selectedEntityId: null });
      }),
      on(this.entityActions.setCurrentEntity, (state: IState<T>, { id }) => {
        return { ...state, selectedEntityId: id };
      }),
      on(this.entityActions.setTotalCount, (state: IState<T>, { totalCount }) => {
        return { ...state, totalCount: totalCount };
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
  private entityActions: EntityActions<T>;

  entities:Observable<T[]>;

  constructor(private configurator: CrudEntityConfigurator<T>, private httpClient: HttpClient, private store: Store<{}>) {
    this.entityActions = configurator.entityActions;
    this.entities = store.pipe(select(configurator.entitySelectors.selectAll));
  }

  addMany(entities: T[]) {
    this.store.dispatch(this.entityActions.addEntities({ entities: entities }));
  }
  getAll() {
    
  }
}


@Injectable()
export class EntityServiceFabric {
  constructor(private registry: ConfiguratorRegistry, private httpClient: HttpClient, private store: Store<{}>) { }

  public getService<T extends IEntityBase>(entityName: string): IEntityService<T> {
    const configurator = this.registry.getCrudEntityConfigurator<T>(entityName);
    return new EntityService<T>(<CrudEntityConfigurator<T>>configurator,this.httpClient,this.store);
  }
}
