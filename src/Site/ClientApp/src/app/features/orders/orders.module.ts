import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { AutocompleteItemEditComponent } from './autocomplete-item/autocomplete-item-edit.component';
import { AutocompleteItemListComponent } from './autocomplete-item/autocomplete-item-list.component';
import { OrderStatusEditComponent } from './order-status/order-status-edit.component';
import { OrderStatusListComponent } from './order-status/order-status-list.component';
import { UiCommonModule } from "../../ui-common/ui-common.module";
import { CrudsEntityMetadata, CrudEntityConfigurator, RaCrudsModule } from "../ra-cruds/ra-cruds.module";
import { AutocompleteItem } from "./autocomplete-item/autocomplete-item.models";
import { BranchListComponent } from "./branch/branch-list.component";
import { BranchEditComponent } from "./branch/branch-edit.component";
import { OrderTypeListComponent } from "./order-type/order-type-list.component";
import { OrderTypeEditComponent } from "./order-type/order-type-edit.component";


const routes: Routes = [
  { path: '', component: OrdersComponent },
  { path: 'autocomplete-item', component: AutocompleteItemListComponent },
	{ path: 'autocomplete-item/:id', component: AutocompleteItemEditComponent },
	{ path: 'order-status', component: OrderStatusListComponent },
	{ path: 'order-status/:id', component: OrderStatusEditComponent },
	{ path: 'branch', component: BranchListComponent },
	{ path: 'branch/:id', component: BranchEditComponent },
	{ path: 'order-type', component: OrderTypeListComponent },
	{ path: 'order-type/:id', component: OrderTypeEditComponent }
];


const config: CrudsEntityMetadata = {
  "autocompleteItems": new CrudEntityConfigurator<AutocompleteItem>("autocompleteItem"),
	"orderStatuses": new CrudEntityConfigurator<AutocompleteItem>("orderStatus"),
	"branches": new CrudEntityConfigurator<AutocompleteItem>("branch"),
	"orderTypes": new CrudEntityConfigurator<AutocompleteItem>("orderType"),
}

@NgModule({
	declarations: [OrdersComponent,
		AutocompleteItemEditComponent,
		AutocompleteItemListComponent,
		OrderStatusEditComponent,
		OrderStatusListComponent,
		BranchListComponent,
		BranchEditComponent,
		OrderTypeListComponent,
    OrderTypeEditComponent
  ],
  imports: [
    CommonModule,
    UiCommonModule,
    RouterModule.forChild(routes),
    RaCrudsModule.forFeature("orders", config)
  ],
  providers: []
})
export class OrdersModule {
  constructor() {
    
  }

}
