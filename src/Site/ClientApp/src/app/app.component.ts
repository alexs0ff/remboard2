import { Component } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectNavigationPaneVisible, navigationPaneToggle } from "./menu/menu.module";
import { AppInitService } from "./app-init.service";
import { selectIsAuthenticated } from "./auth/auth.module";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  navigationPaneVisible$:Observable<boolean>;
  isAuthenticated$:Observable<boolean>;

  constructor(private store: Store<{}>, private appInitService: AppInitService) {
    this.navigationPaneVisible$ = store.pipe(select(selectNavigationPaneVisible));
    this.isAuthenticated$ = store.pipe(select(selectIsAuthenticated));
    appInitService.init();
  }

}
