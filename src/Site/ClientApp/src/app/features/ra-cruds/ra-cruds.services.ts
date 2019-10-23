import { Injectable, Inject, InjectionToken } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import * as uuid from 'uuid';
import { Action, createAction, props, ActionReducer, on, createReducer, createSelector, MemoizedSelector } from '@ngrx/store';
import { EntityState, createEntityAdapter } from '@ngrx/entity';
import { IEntityBase, CrudAdapter, IState, IEntityService, ICrudEntityConfigurator, PagedResult, QueryParams,
  EntityResponse,
  EntityCorrelationIds
} from "./ra-cruds.models"
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { EntityActions, loadAllEntities, createEntity, updateEntity, deleteEntity, loadWithQueryEntities, loadByIdEntity } from "./ra-cruds.actions";
import { RaUtils } from "./ra-cruds.utils";


class EntitySelectors<T extends IEntityBase> {
	selectAll: MemoizedSelector<any, T[]>;
	currentEntity: MemoizedSelector<any, T>;
	hasError: MemoizedSelector<any, boolean>;
	totalCount: MemoizedSelector<any, number>;
	isLoading: MemoizedSelector<any, boolean>;
	errorResponse: MemoizedSelector<any, EntityResponse>;
	lastRemovedIds: MemoizedSelector<any, string[] | null>;
	lastAddedIds: MemoizedSelector<any, EntityCorrelationIds[] | null>;


	constructor(featureName: string, private entitiesName: string, adapter: CrudAdapter<T>) {
		const { selectIds, selectEntities, selectAll, selectTotal, } = adapter.getSelectors();

		const getAppState = (state: any) => state[featureName];

		const getModuleStateAny = (state: any) => getAppState(state)[entitiesName];
		const getModuleState = (state: any) => <IState<T>>getModuleStateAny(state);

		const getSelectedEntityId = (state: IState<T>) => state.selectedId;

		const getModuleEntities = (state: any) => selectEntities(getModuleState(state));
		const getModuleSelectedEntityId = (state: any) => getSelectedEntityId(getModuleState(state));

		this.selectAll = createSelector(getModuleState, selectAll);
		this.totalCount = createSelector(getModuleState, (i) => i.totalCount);
		this.isLoading = createSelector(getModuleState, (i) => i.loading);
		this.hasError = createSelector(getModuleState, (i) => i.hasError);
		this.errorResponse = createSelector(getModuleState, (i) => i.error);
		this.lastRemovedIds = createSelector(getModuleState, (i) => i.lastRemovedIds);
		this.lastAddedIds = createSelector(getModuleState, (i) => i.lastAddedIds);

		this.currentEntity = createSelector(getModuleEntities,
			getModuleSelectedEntityId,
			(entities, currentId) => entities[currentId]);
	}


}


export class CrudEntityConfigurator<T extends IEntityBase> implements ICrudEntityConfigurator<T> {

  private adapter: CrudAdapter<T>;
  private initialState: IState<T>;

  entityActions: EntityActions<T>;
  entitySelectors: EntitySelectors<T>;
  entityReducer: ActionReducer<EntityState<T>, Action>;

  constructor(public singleEntityName:string) {}

  public configure(featureName: string, entitiesName: string) {
    this.adapter = createEntityAdapter<T>();
    this.initialState = this.adapter.getInitialState({
      selectedId: null,
      totalCount: 0,
      loading: false,
      hasError: false,
		  error: null,
		  lastRemovedIds: null,
      lastAddedIds:null
    });

    this.entityActions = new EntityActions<T>(entitiesName);

    this.entitySelectors = new EntitySelectors<T>(featureName, entitiesName, this.adapter);

    this.entityReducer = createReducer(
      this.initialState,
		on(this.entityActions.addEntity, (state, { entity, correlationId }) => {
			return this.adapter.addOne(entity, <IState<T>>{ ...state, loading: false, lastAddedIds: [{ entityId: entity.id, correlationId: correlationId}] });
      }),
      on(this.entityActions.upsertEntity, (state, { entity }) => {
        return this.adapter.upsertOne(entity, <IState<T>>{ ...state, loading: false });
      }),
      on(this.entityActions.upsertEntityAndSetCurrentId, (state, { entity }) => {
        return this.adapter.upsertOne(entity, <IState<T>>{ ...state, loading: false, selectedId:entity.id});
      }),
      on(this.entityActions.addEntities, (state, { entities }) => {
        return this.adapter.addMany(entities, state);
      }),
      on(this.entityActions.upsertEntities, (state, { entities }) => {
        return this.adapter.upsertMany(entities, state);
      }),
      on(this.entityActions.updateEntity, (state, { entity }) => {
        return this.adapter.updateOne(entity, <IState<T>>{ ...state, loading: false });
      }),
      on(this.entityActions.updateEntities, (state, { entities }) => {
        return this.adapter.updateMany(entities, state);
      }),
      on(this.entityActions.mapEntities, (state, { entityMap }) => {
        return this.adapter.map(entityMap, state);
      }),
      on(this.entityActions.deleteEntity, (state, { id }) => {
        return this.adapter.removeOne(id, <IState<T>>{ ...state, loading: false,lastRemovedIds:[id] });
      }),
      on(this.entityActions.deleteEntities, (state, { ids }) => {
		  return this.adapter.removeMany(ids, <IState<T>>{ ...state, loading: false, lastRemovedIds: ids});
      }),
      on(this.entityActions.deleteEntitiesByPredicate, (state, { predicate }) => {
        return this.adapter.removeMany(predicate, state);
      }),
      on(this.entityActions.loadEntities, (state, { entities, totalCount }) => {
        return this.adapter.addAll(entities, <IState<T>>{ ...state, totalCount: totalCount, loading: false  });
      }),
      on(this.entityActions.clearEntities, (state: IState<T>) => {
        return this.adapter.removeAll({ ...state, selectedEntityId: null });
      }),
      on(this.entityActions.setCurrentEntity, (state: IState<T>, { id }) => {
        return { ...state, selectedEntityId: id };
      }),
      on(this.entityActions.setTotalCount, (state: IState<T>, { totalCount }) => {
        return { ...state, totalCount: totalCount };
      }),
      on(this.entityActions.startApiFetch, (state: IState<T>) => {
        return { ...state, loading:true,error:null,hasError:false };
      }),
      on(this.entityActions.setApiError, (state: IState<T>, { error }) => {
        return { ...state, hasError:true,error:error };
      }),
    );

    
  }
}

const configurators: { [key: string]: ICrudEntityConfigurator<any> } = {};

export const CONFIGURATORS_STORAGE = new InjectionToken<{ [key: string]: ICrudEntityConfigurator<any>}>('Configurators registry Storage', {
	providedIn: 'root',
	factory: () => configurators
});

@Injectable({ providedIn: 'root',})
export class ConfiguratorRegistry {

	constructor(@Inject(CONFIGURATORS_STORAGE) public configurators: { [key: string]: ICrudEntityConfigurator<any> }) {}

	public static add(entitiesName: string, configurator: ICrudEntityConfigurator<any>) {
		configurators[entitiesName] = configurator;
	}

	public getCrudEntityConfigurator<T extends IEntityBase>(entitiesName: string): ICrudEntityConfigurator<T> {
		if (!this.configurators.hasOwnProperty(entitiesName)) {
			throw "entity configurator of '" + entitiesName + "' doesn't exist";
		}

		return this.configurators[entitiesName];
	}

	public getEntityActions(entitiesName: string): EntityActions<any> {
		const configurator = <CrudEntityConfigurator<any>>this.getCrudEntityConfigurator(entitiesName);

		return configurator.entityActions;
	}
}


export class EntityService<T extends IEntityBase> implements IEntityService<T> {
	private entityActions: EntityActions<T>;

	entities: Observable<T[]>;

	totalLength: Observable<number>;

	isLoading: Observable<boolean>;

	currentEntity: Observable<T>;

	hasError: Observable<boolean>;

	errorResponse: Observable<EntityResponse>;

	lastRemovedIds: Observable<string[] | null>;

	lastAddedIds: Observable<EntityCorrelationIds[] | null>;

	constructor(private configurator: CrudEntityConfigurator<T>,
		private store: Store<{}>,
		private entitiesName: string) {
		this.entityActions = configurator.entityActions;
		this.entities = store.pipe(select(configurator.entitySelectors.selectAll));
		this.totalLength = store.pipe(select(configurator.entitySelectors.totalCount));
		this.isLoading = store.pipe(select(configurator.entitySelectors.isLoading));
		this.currentEntity = store.pipe(select(configurator.entitySelectors.currentEntity));
		this.hasError = store.pipe(select(configurator.entitySelectors.hasError));
		this.errorResponse = store.pipe(select(configurator.entitySelectors.errorResponse));
		this.lastRemovedIds = store.pipe(select(configurator.entitySelectors.lastRemovedIds));
		this.lastAddedIds = store.pipe(select(configurator.entitySelectors.lastAddedIds));
	}

	addMany(entities: T[]) {
		this.store.dispatch(this.entityActions.addEntities({ entities: entities }));
	}

	getAll() {
		this.store.dispatch(this.entityActions.startApiFetch());
		this.store.dispatch(loadAllEntities({ entitiesName: this.entitiesName }));
	}

	getWithQuery(queryParams: QueryParams) {
		this.store.dispatch(this.entityActions.startApiFetch());
		this.store.dispatch(loadWithQueryEntities({ entitiesName: this.entitiesName, queryParams: queryParams }));
	}

	getById(id: string) {
		this.store.dispatch(this.entityActions.startApiFetch());
		this.store.dispatch(loadByIdEntity({ entitiesName: this.entitiesName, id: id }));
	}

	add(entity: T): string {
		const correlationId = uuid.v4();
		this.store.dispatch(this.entityActions.startApiFetch());
		this.store.dispatch(createEntity({
			entitiesName: this.entitiesName,
			entity: entity,
			correlationId: correlationId
		}));
		return correlationId;
	}

	update(entity: T) {
		this.store.dispatch(this.entityActions.startApiFetch());
		this.store.dispatch(updateEntity({ entitiesName: this.entitiesName, entity: entity, id: entity.id }));
	}

	delete(id: string) {
		this.store.dispatch(this.entityActions.startApiFetch());
		this.store.dispatch(deleteEntity({ entitiesName: this.entitiesName, id: id }));
	}
}


@Injectable()
export class EntityServiceFactory {
  constructor(private registry: ConfiguratorRegistry, private store: Store<{}>) { }

  public getService<T extends IEntityBase>(entitiesName: string): IEntityService<T> {
    const configurator = this.registry.getCrudEntityConfigurator<T>(entitiesName);
    return new EntityService<T>(<CrudEntityConfigurator<T>>configurator,this.store,entitiesName);
  }
}

export interface IEntityApiService{
  getAll(): Observable<PagedResult>;
  getWithQuery(queryParams: QueryParams): Observable<PagedResult>;
  getById<T extends IEntityBase>(id: string): Observable<T>;
  add<T extends IEntityBase>(newEntity: T): Observable<T>;
  update<T extends IEntityBase>(id: string, entity: T): Observable<T>;
  delete(id: string): Observable<any>;
}

class EntityApiService implements IEntityApiService {
  constructor(private entitiesName:string,private entitySingleName:string,private httpClient: HttpClient) {}

  getAll(): Observable<PagedResult> {
    return this.httpClient.get<PagedResult>("api/" + this.entitiesName);
  }

  getWithQuery(queryParams: QueryParams): Observable<PagedResult> {
    const httpParams = RaUtils.toHttpParams(queryParams);
    return this.httpClient.get<PagedResult>("api/" + this.entitiesName, { params:httpParams });
  }

  getById<T extends IEntityBase>(id:string): Observable<T> {
    return this.httpClient.get<T>("api/" + this.entitySingleName + "/" + id);
  }

  add<T extends IEntityBase>(newEntity: T): Observable<T> {
    return this.httpClient.post<T>("api/" + this.entitySingleName, newEntity);
  }

  update<T extends IEntityBase>(id:string,entity: T): Observable<T> {
    return this.httpClient.put<T>("api/" + this.entitySingleName+"/"+id, entity);
  }

  delete(id: string): Observable<any> {
    return this.httpClient.delete("api/" + this.entitySingleName + "/" + id);
  }
}

@Injectable()
export class EntityServiceApiFactory{
  constructor(private registry: ConfiguratorRegistry, private httpClient: HttpClient) { }

  getApiService(entitiesName: string, singleEntityName?: string): IEntityApiService {

    if (singleEntityName == null) {
      const configurator = this.registry.getCrudEntityConfigurator(entitiesName);
      singleEntityName = configurator.singleEntityName;
    }
    return new EntityApiService(entitiesName, singleEntityName, this.httpClient);
  }
  
}
