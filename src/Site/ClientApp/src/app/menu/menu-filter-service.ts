import { Injectable } from '@angular/core';
import { NavigationGroup, navigationPaneItems } from "./navigation-pane/navigation-pane.model";
import { of, Observable } from 'rxjs';

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
