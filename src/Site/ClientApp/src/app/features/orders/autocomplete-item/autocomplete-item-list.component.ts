import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AutocompleteItem } from "./autocomplete-item.models";
import { EntityServiceFabric, IEntityService } from "../../ra-cruds/ra-cruds.module";
import { QueryParamsConfigurator } from "../../ra-cruds/ra-cruds.utils";
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'autocomplete-item-list',
  template: `
<div>
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
  <mat-paginator [pageSizeOptions]="[10, 25, 50]" showFirstLastButtons [pageSize]="pageSize" [length]="totalLength$ | async" (page)="onPaginateChange($event)">></mat-paginator>
</div>
  `,
  styles: [`
table {
  width: 100%;
}
`]
})
export class AutocompleteItemListComponent implements OnInit {

  dataSource$: Observable<AutocompleteItem[]>;
  totalLength$: Observable<number>;

  displayedColumns: string[] = ['title', 'autocompleteKindTitle'];

  pageSize = 10;

  private entityService: IEntityService<AutocompleteItem>;

  constructor(entityServiceFabric: EntityServiceFabric) {
    this.entityService = entityServiceFabric.getService("autocompleteItems");
    this.dataSource$ = this.entityService.entities;
    this.totalLength$ = this.entityService.totalLength;
  }

  ngOnInit() {
    const qConfig: QueryParamsConfigurator = new QueryParamsConfigurator();
    qConfig.setCurrentPage(1);
    qConfig.setPageSize(this.pageSize);

    this.entityService.getWithQuery(qConfig.toQueryParams());
  }

  onPaginateChange(event: PageEvent) {
    const qConfig: QueryParamsConfigurator = new QueryParamsConfigurator();
    qConfig.setCurrentPage(event.pageIndex + 1);
    qConfig.setPageSize(event.pageSize);
    this.entityService.getWithQuery(qConfig.toQueryParams());
  }

  test() {
    
  }
}
