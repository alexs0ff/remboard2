import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap,switchMap,catchError, concatMap, withLatestFrom, tap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';
import { navigationPaneSearch, setMenuItems} from "./menu.actions";
import { MenuFilterService } from "./menu-filter-service";


@Injectable()
export class MenuEffects {
  searchItems$ = createEffect(() => this.actions$.pipe(
    ofType(navigationPaneSearch),
    switchMap((e) => this.filterService.filterItems(e.searchText, e.currentUserRoles)
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


