import { Action, createReducer, on } from '@ngrx/store'
import * as MenuActions from "./menu.actions"
import { navigationPaneItems, NavigationGroup } from "./navigation-pane/navigation-pane.model";


export interface MenuState {
  navigationPane: boolean;
  searchText:string;
  menuItems: NavigationGroup[];
}

export interface MenuModuleState {
  menu: MenuState;
}

export const featureKey = "menu";

export const initialState: MenuState = { navigationPane: true, menuItems: navigationPaneItems, searchText:""};


const menuReducer = createReducer(
  initialState,
  on(MenuActions.navigationPaneToggle, (state: MenuState) => ({ ...state, navigationPane: !state.navigationPane })),
  on(MenuActions.setMenuItems, (state: MenuState, { menuItems }) => ({ ...state,menuItems:menuItems})),
  on(MenuActions.navigationPaneSearch, (state: MenuState, { searchText }) => ({ ...state, searchText: searchText})),
);

export function reducer(state: MenuState | undefined, action: Action) {
  return menuReducer(state, action);
}


