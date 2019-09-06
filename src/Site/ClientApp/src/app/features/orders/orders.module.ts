import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { AutocompleteItemEditComponent } from './autocomplete-item/autocomplete-item-edit.component';
import { AutocompleteItemListComponent } from './autocomplete-item/autocomplete-item-list.component';
import { UiCommonModule } from "../../ui-common/ui-common.module";


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
  providers: []
})
export class OrdersModule {
  constructor() {
    
  }

}
