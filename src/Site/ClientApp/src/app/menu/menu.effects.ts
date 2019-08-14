import { Injectable } from '@angular/core';
import { Actions, ofType, createEffect } from '@ngrx/effects';
import { Store, select } from '@ngrx/store';
import { map, mergeMap, catchError, concatMap, withLatestFrom, tap } from 'rxjs/operators';
import { of,Observable } from 'rxjs';
import { navigationPaneSearch, setMenuItems } from "./menu.actions";
import { NavigationGroup, navigationPaneItems } from "./navigation-pane/navigation-pane.model";


@Injectable({ providedIn: 'root' })
export class MenuFilterService {
  public filterItems(searchText: string): Observable<NavigationGroup[]> {
    const groups: NavigationGroup[] = [];

    for (var groupIndex = 0; groupIndex < navigationPaneItems.length; groupIndex++) {
      groups.push({ name: navigationPaneItems[groupIndex].name, items: [] });

      for (var itemIndex = 0; itemIndex < navigationPaneItems[groupIndex].items.length; itemIndex++) {
        if (searchText === null || searchText.length === 0 || navigationPaneItems[groupIndex].items[itemIndex].name.toLowerCase().includes(searchText.toLowerCase())) {
          groups[groupIndex].items.push(navigationPaneItems[groupIndex].items[itemIndex]);
        }
      }
    }
    return of(groups);

  }
}

@Injectable()
export class MenuEffects {
  searchItems$ = createEffect(() => this.actions$.pipe(
      ofType(navigationPaneSearch),
      mergeMap(() => this.filterService.filterItems("")
        .pipe(
          map(navigationGroups => setMenuItems({ menuItems: navigationGroups })),
        ))
    )
  );

  constructor(
    private actions$: Actions,
    private filterService: MenuFilterService
  ) {
    
  }
}

