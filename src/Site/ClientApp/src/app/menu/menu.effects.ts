import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { map, mergeMap,switchMap,catchError, concatMap, withLatestFrom, tap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';
import { navigationPaneSearch, setMenuItems} from "./menu.actions";
import { MenuFilterService } from "./menu-filter-service";
import { Store, select } from "@ngrx/store";
import { selectCurrentUser, authSignedIn } from "../auth/auth.module";


@Injectable()
export class MenuEffects {
  searchItems$ = createEffect(() => this.actions$.pipe(
    ofType(navigationPaneSearch),
    withLatestFrom(this.store.pipe(select(selectCurrentUser))),
    switchMap(([e,user]) => this.filterService.filterItems(e.searchText, user)
        .pipe(
          map(navigationGroups => setMenuItems({ menuItems: navigationGroups})),
          catchError(() => EMPTY)
        ))
    )
  );

  authSignedIns$ = createEffect(() => this.actions$.pipe(
    ofType(authSignedIn), tap(a => this.store.dispatch(navigationPaneSearch({ searchText:''})))
    ), { dispatch: false }
  );
  constructor(
    private actions$: Actions,
    private filterService: MenuFilterService,
    private store: Store<{}>
  ) {
    
  }
}


