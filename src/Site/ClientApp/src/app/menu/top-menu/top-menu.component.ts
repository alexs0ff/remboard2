import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs";
import { Store,select } from "@ngrx/store";
import { navigationPaneToggle } from "../menu.actions";


@Component({
  selector: 'top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.scss']
})
export class TopMenuComponent implements OnInit {
  
  constructor(private store: Store<{}>) {
    
  }

  ngOnInit(): void {

  }

  toggleNavigationPane() {
    this.store.dispatch(navigationPaneToggle());
  }
}
