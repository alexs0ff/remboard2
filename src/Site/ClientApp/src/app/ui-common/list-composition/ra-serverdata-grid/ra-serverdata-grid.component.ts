import { Component, OnInit,Input } from '@angular/core';
import { EntityServiceFabric, IEntityService } from "../../../features/ra-cruds/ra-cruds.module";
import { Observable } from 'rxjs';
import { AutocompleteItem } from "../../../features/orders/autocomplete-item/autocomplete-item.models";
import { QueryParamsConfigurator } from "../../../features/ra-cruds/ra-cruds.utils";
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { RaServerDataGridModel } from "../list-composition.models";

@Component({
  selector: 'ra-serverdata-grid',
  templateUrl: './ra-serverdata-grid.component.html',
  styleUrls: ['./ra-serverdata-grid.component.scss']
})
export class RaServerdataGridComponent implements OnInit {

  dataSource$: Observable<AutocompleteItem[]>;
  totalLength$: Observable<number>;
  isLoading$: Observable<boolean>;

  displayedColumns: string[] = [];

  pageSize = 10;
  private currentPage: number = 1;
  private sortedColumn: string = "";
  private sortDirection: string = "";
  private entityService: IEntityService<AutocompleteItem>;

  @Input()
  model: RaServerDataGridModel;

  constructor(private entityServiceFabric: EntityServiceFabric) {
    
  }

  ngOnInit() {
    this.entityService = this.entityServiceFabric.getService(this.model.entitiesName);
    this.dataSource$ = this.entityService.entities;
    this.totalLength$ = this.entityService.totalLength;
    this.isLoading$ = this.entityService.isLoading;
    this.displayedColumns = this.model.columns.map(i => i.id);

    if (this.model.pageSize) {
        this.pageSize = this.model.pageSize;
    }

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

    if (this.sortedColumn.length > 0) {
      let isAscending = this.sortDirection === 'asc' || this.sortDirection === '';
      qConfig.setSort(this.sortedColumn, isAscending);
    }
    this.entityService.getWithQuery(qConfig.toQueryParams());
  }
}
