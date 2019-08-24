import { Injectable } from '@angular/core';
import { NavigationGroup, navigationPaneItems } from "./navigation-pane/navigation-pane.model";
import { of, Observable } from 'rxjs';
import { UserInfo } from "../auth/auth.module";


@Injectable({ providedIn: 'root' })
export class MenuFilterService {
  public filterItems(searchText: string,user:UserInfo): Observable<NavigationGroup[]> {
    const groups: NavigationGroup[] = [];
    //current user can have only a role.
    let role: string = '';

    if (user && user.roles && user.roles.length) {
      role = user.roles[0];
    }

    for (var groupIndex = 0; groupIndex < navigationPaneItems.length; groupIndex++) {

      if (navigationPaneItems[groupIndex].roles.filter(r=>r===role).length===0) {
        continue;
      }
      groups.push({ name: navigationPaneItems[groupIndex].name, items: [], roles: navigationPaneItems[groupIndex].roles });

      for (var itemIndex = 0; itemIndex < navigationPaneItems[groupIndex].items.length; itemIndex++) {

        if (navigationPaneItems[groupIndex].items[itemIndex].roles.filter(r=>r===role).length===0) {
          continue;
        }

        if (searchText === null || searchText.length === 0 || navigationPaneItems[groupIndex].items[itemIndex].name.toLowerCase().includes(searchText.toLowerCase())) {
          groups[groupIndex].items.push(navigationPaneItems[groupIndex].items[itemIndex]);
        }
      }
    }
    return of(groups);

  }
}
