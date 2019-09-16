import { NgModule, ModuleWithProviders,Type } from '@angular/core';
import { StoreModule } from "@ngrx/store";
import { ActionReducerMap, } from '@ngrx/store';
import { CrudsEntityMetadata, QueryParams} from "./ra-cruds.models";
import { QueryParamsConfigurator} from "./ra-cruds.utils";
import { ConfiguratorRegistry, EntityServiceFabric, CrudEntityConfigurator, EntityServiceApiFactory } from "./ra-cruds.services";
import { IEntityBase, IEntityService, ICrudEntityConfigurator, PagedResult } from "./ra-cruds.models"
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

  static forFeature(featureName:string,config: CrudsEntityMetadata): ModuleWithProviders[] {
    const ruducersMap: ActionReducerMap<IFeatureState> = {}

    const registry: ConfiguratorRegistry = new ConfiguratorRegistry();

    for (let key in config) {
      let configurator = config[key];
      configurator.configure(featureName,key);
      ruducersMap[key] = configurator.entityReducer;

      registry.add(key, configurator);
    }

    let storeModule = StoreModule.forFeature(featureName, ruducersMap);

    const effectsModule = EffectsModule.forFeature([RaCrudsEntityEffects]);

    const raCrudsModule: ModuleWithProviders = { ngModule: RaCrudsModule, providers: [EntityServiceApiFactory, EntityServiceFabric, { provide: ConfiguratorRegistry, useValue: registry }]}

    return [raCrudsModule,storeModule,effectsModule];
  }
}

export { RaCrudsModule, CrudsEntityMetadata, IEntityBase, IEntityService, CrudEntityConfigurator, EntityServiceFabric, EntityServiceApiFactory, QueryParams, QueryParamsConfigurator, PagedResult}
