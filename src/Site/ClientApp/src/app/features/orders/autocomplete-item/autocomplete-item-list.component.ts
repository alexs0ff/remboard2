import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AutocompleteItem } from "./autocomplete-item.models";
import { EntityServiceFabric, IEntityService } from "../../ra-cruds/ra-cruds.module";

@Component({
  selector: 'autocomplete-item-list',
  template: `
<button (click)="test()"> test</button>
<table mat-table [dataSource]="dataSource$">
  
  <ng-container matColumnDef="title">
    <th mat-header-cell *matHeaderCellDef>Title</th>
    <td mat-cell *matCellDef="let element"> {{element.title}} </td>
  </ng-container>

  <ng-container matColumnDef="autocompleteKindTitle">
    <th mat-header-cell *matHeaderCellDef> Kind </th>
    <td mat-cell *matCellDef="let element"> {{element.autocompleteKindTitle}} </td>
  </ng-container>

  <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
  <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
</table>
  `,
  styles: []
})
export class AutocompleteItemListComponent implements OnInit {

  dataSource$: Observable<AutocompleteItem[]>;

  displayedColumns: string[] = ['title', 'autocompleteKindTitle'];

  private entityService: IEntityService<AutocompleteItem>;

  constructor(entityServiceFabric: EntityServiceFabric) {
    this.entityService = entityServiceFabric.getService("autocompleteItems");
    this.dataSource$ = this.entityService.entities;
  }

  ngOnInit() {
    this.entityService.getAll();
  }

  test() {
    
  }
}
