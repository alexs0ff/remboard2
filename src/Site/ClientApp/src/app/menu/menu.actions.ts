import { createAction,props } from '@ngrx/store'
import { NavigationGroup } from "./navigation-pane/navigation-pane.model";

export const navigationPaneToggle = createAction('[Navigation Pane] Toggle');
export const navigationPaneSearch = createAction('[Navigation Pane] Search', props<{searchText:string}>());
export const setMenuItems = createAction('[Navigation Pane] Set Menu Items', props<{ menuItems: NavigationGroup[]}>());
