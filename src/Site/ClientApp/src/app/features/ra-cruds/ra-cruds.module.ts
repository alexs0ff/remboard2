import { NgModule, ModuleWithProviders,Type } from '@angular/core';
import { StoreModule } from "@ngrx/store";
import { ActionReducerMap, } from '@ngrx/store';
import { CrudsEntityMetadata, IEntityBase as IEntityBase1 } from "./ra-cruds.models";
import { ConfiguratorRegistry, EntityServiceFabric, CrudEntityConfigurator, EntityServiceApiFactory } from "./ra-cruds.services";
import { IEntityBase, IEntityService, ICrudEntityConfigurator, } from "./ra-cruds.models"
import { EffectsModule } from '@ngrx/effects';
import { RaCrudsEntityEffects, RaCrudsEntityEffects2 } from "./ra-cruds.effects";


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
    storeModule.providers.push({ provide: ConfiguratorRegistry, useValue: registry });


    storeModule.providers.push({ provide: EntityServiceFabric, useClass: EntityServiceFabric });

    const effectsModule = EffectsModule.forFeature([RaCrudsEntityEffects]);

    const raCrudsModule: ModuleWithProviders = { ngModule: RaCrudsModule, providers: [EntityServiceApiFactory]}

    return [raCrudsModule,storeModule,effectsModule];
  }
}


export { RaCrudsModule, CrudsEntityMetadata, IEntityBase, IEntityService, CrudEntityConfigurator, EntityServiceFabric}
