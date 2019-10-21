import { NgModule, ModuleWithProviders,Type } from '@angular/core';
import { StoreModule, Action  } from "@ngrx/store";
import { ActionReducerMap, } from '@ngrx/store';
import { CrudsEntityMetadata, QueryParams} from "./ra-cruds.models";
import { QueryParamsConfigurator} from "./ra-cruds.utils";
import { ConfiguratorRegistry, EntityServiceFabric, CrudEntityConfigurator, EntityServiceApiFactory } from "./ra-cruds.services";
import { IEntityBase, IEntityService, ICrudEntityConfigurator, PagedResult, EntityResponse, ValidationError } from "./ra-cruds.models"
import { EffectsModule } from '@ngrx/effects';
import { RaCrudsEntityEffects  } from "./ra-cruds.effects";


interface IFeatureState {

}


@NgModule({
	declarations: [],
	imports: [],
	providers: []
})
class RaCrudsModule {

	static forFeature(featureName: string, config: CrudsEntityMetadata): ModuleWithProviders[] {
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

	static prepareReducersMap(config: CrudsEntityMetadata, currentMap: any) {
		for (let key in config) {
			let configurator = config[key];
			configurator.configure("RaEntities", key);
			currentMap[key] = configurator.entityReducer;
			ConfiguratorRegistry.add(key, configurator);
		}
	}

	static forRoot(): ModuleWithProviders[] {
		const effectsModule = EffectsModule.forFeature([RaCrudsEntityEffects]);
		const raCrudsModule: ModuleWithProviders = {
			ngModule: RaCrudsModule,
			providers: [
				EntityServiceApiFactory, EntityServiceFabric
			]
		}

		return [raCrudsModule, effectsModule];
	}

}

export { RaCrudsModule, CrudsEntityMetadata, IEntityBase, IEntityService, CrudEntityConfigurator, EntityServiceFabric, EntityServiceApiFactory, QueryParams, QueryParamsConfigurator, PagedResult, EntityResponse, ValidationError}
