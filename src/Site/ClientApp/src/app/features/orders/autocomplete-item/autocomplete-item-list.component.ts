import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AutocompleteItem } from "./autocomplete-item.models";

@Component({
  selector: 'autocomplete-item-list',
  template: `
<table mat-table [dataSource]="dataSource$">
  
  <ng-container matColumnDef="title">
    <th mat-header-cell *matHeaderCellDef>Title</th>
    <td mat-cell *matCellDef="let element"> {{element.title}} </td>
  </ng-container>

  <ng-container matColumnDef="autocompleteKindId">
    <th mat-header-cell *matHeaderCellDef> Kind </th>
    <td mat-cell *matCellDef="let element"> {{element.autocompleteKindId}} </td>
  </ng-container>

  <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
  <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
</table>
  `,
  styles: []
})
export class AutocompleteItemListComponent implements OnInit {

  dataSource$: Observable<AutocompleteItem[]>;

  displayedColumns: string[] = ['title', 'autocompleteKindId'];

  constructor() {
  }

  ngOnInit() {
  }

}
