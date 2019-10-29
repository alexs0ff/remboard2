import { NgModule, ModuleWithProviders,Type } from '@angular/core';
import { StoreModule, Action  } from "@ngrx/store";
import { ActionReducerMap, } from '@ngrx/store';
import { CrudsEntityMetadata, QueryParams} from "./ra-cruds.models";
import { QueryParamsConfigurator} from "./ra-cruds.utils";
import { ConfiguratorRegistry, EntityServiceFactory, CrudEntityConfigurator, EntityServiceApiFactory } from "./ra-cruds.services";
import { IEntityBase, IEntityService, ICrudEntityConfigurator, PagedResult, EntityResponse, ValidationError } from "./ra-cruds.models"
import { EffectsModule } from '@ngrx/effects';
import { RaCrudsEntityEffects  } from "./ra-cruds.effects";
import { EntitySchemaServiceApiFactory, EntitySchemaServiceFactory as EntitySchemaServiceFabric, EntitySchemaConfiguratorRegistry, EntitySchemaConfigurator, EntitySchemaServiceFactory } from "./grid/ra-schema-cruds.services";
import { EntitySchemaEffects } from "./grid/ra-schema-cruds.effects";
import { EntitySchemaMetadata, IEntitySchemaService } from "./grid/ra-schema-cruds.models";


interface IFeatureState {

}


@NgModule({
	declarations: [],
	imports: [],
	providers: []
})
class RaCrudsModule {

	static forCrudsFeature(featureName: string, config: CrudsEntityMetadata): ModuleWithProviders[] {
		const ruducersMap: ActionReducerMap<IFeatureState> = {}

		for (let key in config) {
			let configurator = config[key];
			configurator.configure(featureName, key);
			ruducersMap[key] = configurator.entityReducer;

			ConfiguratorRegistry.add(key, configurator);
		}

		let storeModule = StoreModule.forFeature(featureName, ruducersMap);

		const raCrudsModule: ModuleWithProviders = {
			ngModule: RaCrudsModule,
			providers :[]
		}

		return [raCrudsModule, storeModule];
	}

	static forSchemaFeature(featureName: string, configSchema: EntitySchemaMetadata): ModuleWithProviders[] {
		const ruducersMap: ActionReducerMap<IFeatureState> = {}
	
		for (let key in configSchema) {
			let configuratorSchema = configSchema[key];
			configuratorSchema.configure(featureName, key);
			ruducersMap[key] = configuratorSchema.entityReducer;
			EntitySchemaConfiguratorRegistry.add(key, configuratorSchema);
		}

		let storeModule = StoreModule.forFeature(featureName, ruducersMap);

		const raCrudsModule: ModuleWithProviders = {
			ngModule: RaCrudsModule,
			providers: []
		}

		return [raCrudsModule, storeModule];
	}

	static prepareReducersMap(config: CrudsEntityMetadata, configSchema: EntitySchemaMetadata, currentMap: any) {
		for (let key in config) {
			let configurator = config[key];
			configurator.configure("RaEntities", key);
			currentMap[key] = configurator.entityReducer;
			ConfiguratorRegistry.add(key, configurator);
		}

		for (let key in configSchema) {
			let configuratorSchema = configSchema[key];
			configuratorSchema.configure("RaEntitySchemas", key);
			currentMap[key] = configuratorSchema.entityReducer;
			EntitySchemaConfiguratorRegistry.add(key, configuratorSchema);
		}
	}

	static forRoot(): ModuleWithProviders[] {
		const effectsModule = EffectsModule.forFeature([RaCrudsEntityEffects, EntitySchemaEffects]);
		const raCrudsModule: ModuleWithProviders = {
			ngModule: RaCrudsModule,
			providers: [
				EntityServiceApiFactory, EntityServiceFactory, EntitySchemaServiceApiFactory, EntitySchemaServiceFabric
			]
		}

		return [raCrudsModule, effectsModule];
	}

}

export {
	RaCrudsModule,
	CrudsEntityMetadata,
	IEntityBase,
	IEntityService,
	CrudEntityConfigurator,
	EntityServiceFactory,
	EntityServiceApiFactory,
	QueryParams,
	QueryParamsConfigurator,
	PagedResult,
	EntityResponse,
	ValidationError,


	EntitySchemaMetadata,
	EntitySchemaConfigurator,
	IEntitySchemaService,
	EntitySchemaServiceFactory
}
