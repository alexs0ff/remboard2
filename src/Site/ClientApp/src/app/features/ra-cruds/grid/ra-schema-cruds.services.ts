import { Injectable, Inject, InjectionToken } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { FilterableEntityBase, IEntitySchemaConfigurator, EntitySchemaState, IEntitySchemaService } from "./ra-schema-cruds.models";
import { Action, ActionReducer, createReducer, on, createSelector, MemoizedSelector, Store, select } from '@ngrx/store';
import { EntitySchemaActions, loadGridModelWithQuery } from "./ra-schema-cruds.actions";
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema-grid.models";
import { RaUtils } from "../ra-cruds.utils";
import { Observable } from 'rxjs';
import { QueryParams } from "../ra-cruds.module";


export class EntitySchemaSelectors<T extends FilterableEntityBase> {
	gridModel: MemoizedSelector<any, RaServerDataGridModel>;

	constructor(featureName: string, entitiesName: string) {

		const getAppState = (state: any) => state[featureName];
		const getModuleStateAny = (state: any) => getAppState(state)[entitiesName];
		const getModuleState = (state: any) => <EntitySchemaState>getModuleStateAny(state);

		this.gridModel = createSelector(getModuleState, (i) => i.model);


	}
}

const entitySchemaConfigurators: { [key: string]: IEntitySchemaConfigurator<any> } = {};

export const ENTITY_SCHEMA_CONFIGURATORS_STORAGE = new InjectionToken<{ [key: string]: IEntitySchemaConfigurator<any> }>('Configurators registry Storage', {
	providedIn: 'root',
	factory: () => entitySchemaConfigurators
});

@Injectable({ providedIn: 'root', })
export class EntitySchemaConfiguratorRegistry {

	constructor(@Inject(ENTITY_SCHEMA_CONFIGURATORS_STORAGE) public configurators: { [key: string]: IEntitySchemaConfigurator<any> }) { }

	public static add(entitiesName: string, configurator: IEntitySchemaConfigurator<any>) {
		entitySchemaConfigurators[entitiesName] = configurator;
	}

	public getEntitySchemaConfigurator<T extends FilterableEntityBase>(entitiesName: string): IEntitySchemaConfigurator<T> {
		if (!this.configurators.hasOwnProperty(entitiesName)) {
			throw "entity schema configurator of '" + entitiesName + "' doesn't exist";
		}

		return this.configurators[entitiesName];
	}

	public getEntityActions(entitiesName: string): EntitySchemaActions<any> {
		const configurator = <EntitySchemaConfigurator<any>>this.getEntitySchemaConfigurator(entitiesName);

		return configurator.entityActions;
	}
}


export class EntitySchemaConfigurator<T extends FilterableEntityBase> implements IEntitySchemaConfigurator<T> {

	constructor(public singleEntityName: string) {}

	entityActions: EntitySchemaActions<T>;

	entityReducer: ActionReducer<EntitySchemaState, Action>;

	entitySchemaSelectors: EntitySchemaSelectors<T>;

	configure(featureName: string, entitiesName: string) {
		this.entityActions = new EntitySchemaActions<T>(entitiesName);

		const initState: EntitySchemaState = { model: null };
		this.entityReducer = createReducer(
			initState,
			on(this.entityActions.updateGridModel, (state: EntitySchemaState, { model }) => ({ ...state, model: model}))
		);

		this.entitySchemaSelectors = new EntitySchemaSelectors<T>(featureName, entitiesName);

	}
}

@Injectable()
export class EntitySchemaServiceFactory {
	constructor(private registry: EntitySchemaConfiguratorRegistry, private store: Store<{}>) { }

	public getService<T extends FilterableEntityBase>(entitiesName: string): IEntitySchemaService<T> {
		const configurator = this.registry.getEntitySchemaConfigurator<T>(entitiesName);
		return new EntitySchemaService<T>(<EntitySchemaConfigurator<T>>configurator, this.store, entitiesName);
	}
}

export class EntitySchemaService<T extends FilterableEntityBase> implements IEntitySchemaService<T> {
	private entityActions: EntitySchemaActions<T>;

	gridModel: Observable<RaServerDataGridModel>;

	constructor(configurator: EntitySchemaConfigurator<T>,
		private store: Store<{}>,
		private entitiesName: string) {
		this.gridModel = store.pipe(select(configurator.entitySchemaSelectors.gridModel));
		this.entityActions = configurator.entityActions;
	}

	getWithQuery(queryParams: QueryParams) {
		this.store.dispatch(loadGridModelWithQuery({ entitiesName: this.entitiesName, queryParams: queryParams,force:true }));
	}

	getIfEmpty() {
		this.store.dispatch(loadGridModelWithQuery({ entitiesName: this.entitiesName, queryParams: null, force: false }));
	}

	updateModel(model: RaServerDataGridModel) {
		this.store.dispatch(this.entityActions.updateGridModel({model:model}));
	}
}

export interface IEntitySchemaApiService {
	getWithQuery(queryParams: QueryParams): Observable<RaServerDataGridModel>;
}

class EntitySchemaApiService implements IEntitySchemaApiService {

	constructor(private entitiesName: string, private entitySingleName: string, private httpClient: HttpClient) { }

	getWithQuery(queryParams): Observable<RaServerDataGridModel> {
		const httpParams = RaUtils.toHttpParams(queryParams);
		return this.httpClient.get<RaServerDataGridModel>("api/" + this.entitySingleName + "/gridSchema",
			{ params: httpParams });
	}
}

@Injectable()
export class EntitySchemaServiceApiFactory {
	constructor(private registry: EntitySchemaConfiguratorRegistry, private httpClient: HttpClient) { }

	getApiService(entitiesName: string, singleEntityName?: string): IEntitySchemaApiService {

		if (singleEntityName == null) {
			const configurator = this.registry.getEntitySchemaConfigurator(entitiesName);
			singleEntityName = configurator.singleEntityName;
		}
		return new EntitySchemaApiService(entitiesName, singleEntityName, this.httpClient);
	}
}
