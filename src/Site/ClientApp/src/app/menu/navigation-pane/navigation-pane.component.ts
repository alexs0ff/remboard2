import { Component, OnInit } from '@angular/core';
import { NavigationGroup } from "./navigation-pane.model";
import { trigger, animate, state, style, transition } from '@angular/animations';
import { Store,select } from "@ngrx/store";
import { Observable } from "rxjs";
import { selectNavigationPaneItems } from "../menu.selectors";

@Component({
  selector: 'navigation-pane',
  template: `
<div class="docs-component-viewer-nav">
  <div class="docs-component-viewer-nav-content">
    <mat-form-field class="docs-component-viewer-navigation-pane-search">
      <mat-label>Поиск</mat-label>
      <input matInput placeholder="Пункт меню">
      <mat-icon matSuffix>search</mat-icon>      
    </mat-form-field>
    <nav *ngFor="let category of items$ | async; let last = last;">      
      <button cdkAccordionItem #panel="cdkAccordionItem" (click)="panel.toggle()" expanded="true"
              class="docs-nav-content-btn"
              [attr.aria-label]="category.name + ', section toggle'"              
              [attr.aria-expanded]="panel.expanded">
        {{category.name}}
        <mat-icon>{{panel.expanded ? 'keyboard_arrow_up' :  'keyboard_arrow_down'}}</mat-icon>
      </button>
      <ul [@bodyExpansion]="panel.expanded ? 'expanded' : 'collapsed'" id="panel-{{category.id}}">
        <li *ngFor="let component of category.items">
          <a [routerLink]="'/'+ component.url"
             routerLinkActive="docs-component-viewer-sidenav-item-selected">
            {{component.name}}
          </a>
        </li>
      </ul>
      <hr *ngIf="!last" />
    </nav>
  </div>
</div>
  `,
  styleUrls: ['./navigation-pane.component.scss'],
  animations: [
    trigger('bodyExpansion', [
      state('collapsed', style({ height: '0px', display: 'none' })),
      state('expanded', style({ height: '*', display: 'block' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4,0.0,0.2,1)')),
    ]),
  ],
})
export class NavigationPaneComponent implements OnInit {

  public  items$: Observable<NavigationGroup[]>;

  constructor(private store: Store<{}>) {
    this.items$ = store.pipe(select(selectNavigationPaneItems));
  }

  ngOnInit() {
  }

}
