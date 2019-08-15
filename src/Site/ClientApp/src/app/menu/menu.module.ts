import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import * as MenuReducer from "./menu.reducer"


import { NavigationPaneComponent } from "./navigation-pane/navigation-pane.component";
import { UiCommonModule } from "../ui-common/ui-common.module";
import { selectNavigationPaneVisible } from "./menu.selectors";
import { navigationPaneToggle, navigationPaneSearch } from "./menu.actions";
import { MenuEffects } from "./menu.effects";
import { EffectsModule } from '@ngrx/effects';
import { TopMenuComponent } from './top-menu/top-menu.component';

@NgModule({
  imports: [
    CommonModule,
    UiCommonModule,
    RouterModule,
    StoreModule.forFeature(MenuReducer.featureKey, MenuReducer.reducer),
  ],
  exports: [NavigationPaneComponent, TopMenuComponent],
  declarations: [NavigationPaneComponent, TopMenuComponent]

})
class MenuModule { }


export { MenuModule, selectNavigationPaneVisible, navigationPaneToggle, navigationPaneSearch}
