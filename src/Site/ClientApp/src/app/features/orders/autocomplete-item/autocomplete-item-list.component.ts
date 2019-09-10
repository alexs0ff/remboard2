import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AutocompleteItem } from "./autocomplete-item.models";
import { EntityServiceFabric, IEntityService } from "../../ra-cruds/ra-cruds.module";
import { QueryParamsConfigurator } from "../../ra-cruds/ra-cruds.utils";
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';

@Component({
  selector: 'autocomplete-item-list',
  template: `
<div>
<table mat-table [dataSource]="dataSource$" matSort (matSortChange)="onSortChange($event)">
  
  <ng-container matColumnDef="title">
    <th mat-header-cell *matHeaderCellDef mat-sort-header>Title</th>
    <td mat-cell *matCellDef="let element"> {{element.title}} </td>
  </ng-container>

  <ng-container matColumnDef="autocompleteKindTitle">
    <th mat-header-cell *matHeaderCellDef mat-sort-header> Kind </th>
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
  private currentPage:number = 1;
  private sortedColumn:string = "";
  private sortDirection:string = "";

  private entityService: IEntityService<AutocompleteItem>;

  constructor(entityServiceFabric: EntityServiceFabric) {
    this.entityService = entityServiceFabric.getService("autocompleteItems");
    this.dataSource$ = this.entityService.entities;
    this.totalLength$ = this.entityService.totalLength;
  }

  ngOnInit() {
    this.refreshData();
  }

  onPaginateChange(event: PageEvent) {
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.refreshData();
  }

  onSortChange(event: Sort) {
    this.sortedColumn = event.active;
    this.sortDirection = event.direction;
    this.refreshData();
  }

  refreshData() {
    const qConfig: QueryParamsConfigurator = new QueryParamsConfigurator();
    qConfig.setCurrentPage(this.currentPage);
    qConfig.setPageSize(this.pageSize);

    if (this.sortedColumn.length>0) {
      let isAscending = this.sortDirection === 'asc' || this.sortDirection === '';
      qConfig.setSort(this.sortedColumn, isAscending);  
    }
    this.entityService.getWithQuery(qConfig.toQueryParams());
  }

  test() {
    
  }
}
