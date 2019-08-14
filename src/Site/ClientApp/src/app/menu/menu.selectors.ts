import { createSelector, createFeatureSelector } from '@ngrx/store';
import { MenuState, MenuModuleState } from "./menu.reducer";

export const getNavigationPane = (state: MenuState) => state.navigationPane;
export const getMenuItems = (state: MenuState) => state.menuItems;

export const selectNavigationPaneVisible = createSelector(
  (moduleState: MenuModuleState) => moduleState.menu,
  getNavigationPane
);

export const selectNavigationPaneItems = createSelector(
  (moduleState: MenuModuleState) => moduleState.menu,
  getMenuItems
);
