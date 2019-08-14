import {NgModule} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


import { CdkAccordionModule } from '@angular/cdk/accordion';
import { NavigationPaneComponent } from "../navigation-pane/navigation-pane.component";

@NgModule({
  imports: [
    MatSidenavModule,
    RouterModule,
    CommonModule,
    BrowserAnimationsModule,
    MatIconModule,
    CdkAccordionModule
  ],
  exports: [NavigationPaneComponent],
  declarations: [NavigationPaneComponent],
})
export class NavigationPaneModule { }
