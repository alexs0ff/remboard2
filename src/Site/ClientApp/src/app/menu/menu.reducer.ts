import { Action, createReducer, on } from '@ngrx/store'
import * as MenuActions from "./menu.actions"
import { navigationPaneItems, NavigationGroup } from "./navigation-pane/navigation-pane.model";


export interface MenuState {
  navigationPane: boolean;
  menuItems: NavigationGroup[];
}

export interface MenuModuleState {
  menu: MenuState;
}

export const featureKey = "menu";

export const initialState: MenuState = { navigationPane: true, menuItems: navigationPaneItems};


const menuReducer = createReducer(
  initialState,
  on(MenuActions.navigationPaneToggle, (state: MenuState) => ({ ...state, navigationPane: !state.navigationPane }))
);

export function reducer(state: MenuState | undefined, action: Action) {
  return menuReducer(state, action);
}


