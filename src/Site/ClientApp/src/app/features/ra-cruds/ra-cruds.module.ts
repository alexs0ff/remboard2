import { NgModule, ModuleWithProviders } from '@angular/core';
import { StoreModule } from "@ngrx/store";
import { ActionReducerMap, } from '@ngrx/store';
import { CrudsEntityMetadata } from "./ra-cruds.models";
import { ConfiguratorRegistry, EntityServiceFabric, CrudEntityConfigurator } from "./ra-cruds.services";
import { IEntityBase, IEntityService, ICrudEntityConfigurator, } from "./ra-cruds.models"

interface IFeatureState {

}


@NgModule({
  declarations: [],
  imports: [],
  providers: []
})
class RaCrudsModule {

  static forFeature(featureName:string,config: CrudsEntityMetadata): ModuleWithProviders {
    const ruducersMap: ActionReducerMap<IFeatureState> = {}

    const registry: ConfiguratorRegistry = new ConfiguratorRegistry();

    for (let key in config) {
      let configurator = config[key];
      configurator.configure(featureName,key);
      ruducersMap[key] = configurator.entityReducer;

      registry.add(key, configurator);
    }

    let result = StoreModule.forFeature(featureName, ruducersMap);
    result.providers.push({ provide: ConfiguratorRegistry, useValue: registry });


    result.providers.push({ provide: EntityServiceFabric, useClass: EntityServiceFabric });

    return result;
  }
}

export { RaCrudsModule, CrudsEntityMetadata, IEntityBase, IEntityService, CrudEntityConfigurator, EntityServiceFabric}
