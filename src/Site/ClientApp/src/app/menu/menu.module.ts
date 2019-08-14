import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as MenuReducer from "./menu.reducer"


import { NavigationPaneComponent } from "./navigation-pane/navigation-pane.component";
import { UiCommonModule } from "../ui-common/ui-common.module";
import { selectNavigationPaneVisible } from "./menu.selectors";
import { navigationPaneToggle } from "./menu.actions";

@NgModule({
  imports: [
    CommonModule,
    UiCommonModule,
    RouterModule,
    StoreModule.forFeature(MenuReducer.featureKey, MenuReducer.reducer),
  ],
  exports: [NavigationPaneComponent],
  declarations: [NavigationPaneComponent],
})
class MenuModule { }


export { MenuModule, selectNavigationPaneVisible, navigationPaneToggle}
