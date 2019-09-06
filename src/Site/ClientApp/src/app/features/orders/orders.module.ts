import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { AutocompleteItemEditComponent } from './autocomplete-item/autocomplete-item-edit.component';
import { AutocompleteItemListComponent } from './autocomplete-item/autocomplete-item-list.component';
import { AutocompleteItemService, autocompleteItemMetadata, AutocompleteItemDataService } from "./autocomplete-item/autocomplete-item.configuration";
import { UiCommonModule } from "../../ui-common/ui-common.module";
import { EntityDefinitionService, EntityDataService } from '@ngrx/data';


const routes: Routes = [
  { path: '', component: OrdersComponent },
  { path: 'autocomplete-item', component: AutocompleteItemListComponent },
  { path: 'autocomplete-item/:id', component: AutocompleteItemEditComponent }
];

@NgModule({
  declarations: [OrdersComponent, AutocompleteItemEditComponent, AutocompleteItemListComponent],
  imports: [
    CommonModule,
    UiCommonModule,
    RouterModule.forChild(routes)
  ],
  providers: [AutocompleteItemService, AutocompleteItemDataService]
})
export class OrdersModule {
  constructor(eds: EntityDefinitionService, entityDataService: EntityDataService, autocompleteItemDataService:AutocompleteItemDataService) {
    eds.registerMetadataMap(autocompleteItemMetadata);
    entityDataService.registerService('autocompleteitem', autocompleteItemDataService);
  }

}
