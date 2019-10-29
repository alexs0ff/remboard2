import { Injectable, Inject, InjectionToken } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Action, ActionReducer, createReducer, on, createSelector, MemoizedSelector, Store, select } from '@ngrx/store';

import { RaUtils } from "../ra-cruds.utils";
import { Observable } from 'rxjs';
import { QueryParams, IEntityBase } from "../ra-cruds.module";
import { EntityEditSchemaState, IEntityEditSchemaConfigurator, IEntityEditSchemaService } from "./ra-edit-schema-cruds.models";
import { RaEntityEdit } from "../../../ra-schema/ra-schema-forms.models";
import { EntityEditSchemaActions, loadEditModelWithQuery } from "./ra-edit-schema-cruds.actions";


export class EntityEditSchemaSelectors<T extends IEntityBase> {
	gridModel: MemoizedSelector<any, RaEntityEdit>;

	constructor(featureName: string, entitiesName: string) {

		const getAppState = (state: any) => state[featureName];
		const getModuleStateAny = (state: any) => getAppState(state)[entitiesName];
		const getModuleState = (state: any) => <EntityEditSchemaState>getModuleStateAny(state);

		this.gridModel = createSelector(getModuleState, (i) => i.model);
	}
}

const entityEditSchemaConfigurators: { [key: string]: IEntityEditSchemaConfigurator<any> } = {};

export const ENTITY_SCHEMA_CONFIGURATORS_STORAGE = new InjectionToken<{ [key: string]: IEntityEditSchemaConfigurator<any> }>('Configurators registry Storage', {
	providedIn: 'root',
	factory: () => entityEditSchemaConfigurators
});

@Injectable({ providedIn: 'root', })
export class EntityEditSchemaConfiguratorRegistry {

	constructor(@Inject(ENTITY_SCHEMA_CONFIGURATORS_STORAGE) public configurators: { [key: string]: IEntityEditSchemaConfigurator<any> }) { }

	public static add(entitiesName: string, configurator: IEntityEditSchemaConfigurator<any>) {
		entityEditSchemaConfigurators[entitiesName] = configurator;
	}

	public getEntityEditSchemaConfigurator<T extends IEntityBase>(entitiesName: string): IEntityEditSchemaConfigurator<T> {
		if (!this.configurators.hasOwnProperty(entitiesName)) {
			throw "entity edit schema configurator of '" + entitiesName + "' doesn't exist";
		}

		return this.configurators[entitiesName];
	}

	public getEntityActions(entitiesName: string): EntityEditSchemaActions<any> {
		const configurator = <EntityEditSchemaConfigurator<any>>this.getEntityEditSchemaConfigurator(entitiesName);

		return configurator.entityActions;
	}
}


export class EntityEditSchemaConfigurator<T extends IEntityBase> implements IEntityEditSchemaConfigurator<T> {

	constructor(public singleEntityName: string) {}

	entityActions: EntityEditSchemaActions<T>;

	entityReducer: ActionReducer<EntityEditSchemaState, Action>;

	entityEditSchemaSelectors: EntityEditSchemaSelectors<T>;

	configure(featureName: string, entitiesName: string) {
		this.entityActions = new EntityEditSchemaActions<T>(entitiesName);

		const initState: EntityEditSchemaState = { model: null };
		this.entityReducer = createReducer(
			initState,
			on(this.entityActions.updateGridModel, (state: EntityEditSchemaState, { model }) => ({ ...state, model: model}))
		);

		this.entityEditSchemaSelectors = new EntityEditSchemaSelectors<T>(featureName, entitiesName);

	}
}

@Injectable()
export class EntityEditSchemaServiceFactory {
	constructor(private registry: EntityEditSchemaConfiguratorRegistry, private store: Store<{}>) { }

	public getService<T extends IEntityBase>(entitiesName: string): IEntityEditSchemaService<T> {
		const configurator = this.registry.getEntityEditSchemaConfigurator<T>(entitiesName);
		return new EntityEditSchemaService<T>(<EntityEditSchemaConfigurator<T>>configurator, this.store, entitiesName);
	}
}

export class EntityEditSchemaService<T extends IEntityBase> implements IEntityEditSchemaService<T> {
	private entityActions: EntityEditSchemaActions<T>;

	editModel: Observable<RaEntityEdit>;

	constructor(configurator: EntityEditSchemaConfigurator<T>,
		private store: Store<{}>,
		private entitiesName: string) {
		this.editModel = store.pipe(select(configurator.entityEditSchemaSelectors.gridModel));
		this.entityActions = configurator.entityActions;
	}

	getWithQuery(queryParams: QueryParams) {
		this.store.dispatch(loadEditModelWithQuery({ entitiesName: this.entitiesName, queryParams: queryParams }));
	}
}

export interface IEntityEditSchemaApiService {
	getWithQuery(queryParams: QueryParams): Observable<RaEntityEdit>;
}

class EntityEditSchemaApiService implements IEntityEditSchemaApiService {

	constructor(private entitiesName: string, private entitySingleName: string, private httpClient: HttpClient) { }

	getWithQuery(queryParams): Observable<RaEntityEdit> {
		const httpParams = RaUtils.toHttpParams(queryParams);
		return this.httpClient.get<RaEntityEdit>("api/" + this.entitySingleName + "/editSchema",
			{ params: httpParams });
	}
}

@Injectable()
export class EntityEditSchemaServiceApiFactory {
	constructor(private registry: EntityEditSchemaConfiguratorRegistry, private httpClient: HttpClient) { }

	getApiService(entitiesName: string, singleEntityName?: string): IEntityEditSchemaApiService {

		if (singleEntityName == null) {
			const configurator = this.registry.getEntityEditSchemaConfigurator(entitiesName);
			singleEntityName = configurator.singleEntityName;
		}
		return new EntityEditSchemaApiService(entitiesName, singleEntityName, this.httpClient);
	}
}
