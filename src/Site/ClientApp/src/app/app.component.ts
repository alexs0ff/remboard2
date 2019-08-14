import { Component } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectNavigationPaneVisible, navigationPaneToggle } from "./menu/menu.module";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  navigationPaneVisible$:Observable<boolean>;

  constructor(private store: Store<{}>) {
    this.navigationPaneVisible$ = store.pipe(select(selectNavigationPaneVisible));
  }

  toggleNavigationPane() {
    this.store.dispatch(navigationPaneToggle());
  }
}
