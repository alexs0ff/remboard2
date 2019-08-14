import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { Store, select } from '@ngrx/store';
import { map, mergeMap,switchMap,catchError, concatMap, withLatestFrom, tap } from 'rxjs/operators';
import { of, Observable, EMPTY } from 'rxjs';
import { navigationPaneSearch, setMenuItems, navigationPaneToggle } from "./menu.actions";
import { NavigationGroup, navigationPaneItems } from "./navigation-pane/navigation-pane.model";
import { MenuFilterService } from "./menu-filter-service";


@Injectable()
export class MenuEffects {
  searchItems$ = createEffect(() => this.actions$.pipe(
      ofType(navigationPaneSearch),
      switchMap((e) => this.filterService.filterItems(e.searchText)
        .pipe(
          map(navigationGroups => setMenuItems({ menuItems: navigationGroups})),
          catchError(() => EMPTY)
        ))
    )
  );

  constructor(
    private actions$: Actions,
    private filterService: MenuFilterService
  ) {
    
  }
}


