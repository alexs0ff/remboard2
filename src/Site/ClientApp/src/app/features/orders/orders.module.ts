import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { AutocompleteItemEditComponent } from './autocomplete-item/autocomplete-item-edit.component';
import { AutocompleteItemListComponent } from './autocomplete-item/autocomplete-item-list.component';
import { UiCommonModule } from "../../ui-common/ui-common.module";
import { CrudsEntityMetadata, CrudEntityConfigurator, RaCrudsModule } from "../ra-cruds/ra-cruds.module";
import { AutocompleteItem } from "./autocomplete-item/autocomplete-item.models";


const routes: Routes = [
  { path: '', component: OrdersComponent },
  { path: 'autocomplete-item', component: AutocompleteItemListComponent },
  { path: 'autocomplete-item/:id', component: AutocompleteItemEditComponent }
];


const config: CrudsEntityMetadata = {
  "autocompleteItems": new CrudEntityConfigurator<AutocompleteItem>(),
}

@NgModule({
  declarations: [OrdersComponent, AutocompleteItemEditComponent, AutocompleteItemListComponent],
  imports: [
    CommonModule,
    UiCommonModule,
    RouterModule.forChild(routes),
    RaCrudsModule.forFeature("orders",config)
  ],
  providers: []
})
export class OrdersModule {
  constructor() {
    
  }

}
