import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { AutocompleteItemEditComponent } from './autocomplete-item/autocomplete-item-edit.component';
import { AutocompleteItemListComponent } from './autocomplete-item/autocomplete-item-list.component';
import { OrderStatusEditComponent } from './order-status/order-status-edit.component';
import { OrderStatusListComponent } from './order-status/order-status-list.component';
import { UiCommonModule } from "../../ui-common/ui-common.module";
import { CrudsEntityMetadata, CrudEntityConfigurator, RaCrudsModule, EntitySchemaConfigurator, EntitySchemaMetadata, EntityEditSchemaMetadata, EntityEditSchemaConfigurator } from "../ra-cruds/ra-cruds.module";
import { AutocompleteItem } from "./autocomplete-item/autocomplete-item.models";
import { BranchListComponent } from "./branch/branch-list.component";
import { BranchEditComponent } from "./branch/branch-edit.component";
import { OrderTypeListComponent } from "./order-type/order-type-list.component";
import { OrderTypeEditComponent } from "./order-type/order-type-edit.component";

import { OrderType } from "./order-type/order-type.models";
import { OrderStatus } from "./order-status/order-status.models";
import { Branch } from "./branch/branch.models";


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
	"orderStatuses": new CrudEntityConfigurator<OrderStatus>("orderStatus"),
	"branches": new CrudEntityConfigurator<Branch>("branch"),
	"orderTypes": new CrudEntityConfigurator<OrderType>("orderType"),
}

const configSchema: EntitySchemaMetadata = {
	"autocompleteItems": new EntitySchemaConfigurator<AutocompleteItem>("autocompleteItem"),
	"branches": new EntitySchemaConfigurator<Branch>("branch"),
	"orderTypes": new EntitySchemaConfigurator<OrderType>("orderType"),
	"orderStatuses": new EntitySchemaConfigurator<OrderStatus>("orderStatus"),
}

const configEditSchema: EntityEditSchemaMetadata = {
	"autocompleteItems": new EntityEditSchemaConfigurator<AutocompleteItem>("autocompleteItem"),
	"branches": new EntityEditSchemaConfigurator<Branch>("branch"),
	"orderTypes": new EntityEditSchemaConfigurator<OrderType>("orderType"),
	"orderStatuses": new EntityEditSchemaConfigurator<OrderStatus>("orderStatus"),
}


@NgModule({
	declarations: [
		OrdersComponent,
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
		RaCrudsModule.forCrudsFeature("orders", config ),
		RaCrudsModule.forSchemaFeature("ordersSchema", configSchema),
		RaCrudsModule.forEditSchemaFeature("ordersEditSchema", configEditSchema),
	],
	providers: []
})
export class OrdersModule {
	constructor() {

	}

}
