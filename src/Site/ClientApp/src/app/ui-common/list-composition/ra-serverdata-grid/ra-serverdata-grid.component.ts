import { Component, OnInit,Input } from '@angular/core';
import { EntityServiceFabric, IEntityService } from "../../../features/ra-cruds/ra-cruds.module";
import { Observable } from 'rxjs';
import { AutocompleteItem } from "../../../features/orders/autocomplete-item/autocomplete-item.models";
import { QueryParamsConfigurator } from "../../../features/ra-cruds/ra-cruds.utils";
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { RaServerDataGridModel } from "../list-composition.models";
import { Router, ActivatedRoute } from '@angular/router'
import { FilterData,FilterStatement } from "../../ra-filter.models";
import { GridModelComposer } from "./ra-serverdata-grid.services";
import { RaGridFlatModel } from "./ra-serverdata-grid.models";

@Component({
	selector: 'ra-serverdata-grid',
	templateUrl: './ra-serverdata-grid.component.html',
	styleUrls: ['./ra-serverdata-grid.component.scss']
})
export class RaServerdataGridComponent implements OnInit {

	dataSource$: Observable<AutocompleteItem[]>;
	totalLength$: Observable<number>;
	isLoading$: Observable<boolean>;

	//displayedColumns: string[] = [];

	pageSize = 10;
	private currentPage: number = 1;
	private sortedColumn: string = "";
	private sortDirection: string = "";
	private entityService: IEntityService<any>;
	private currentFilter: FilterData = null;

	@Input()
	model: RaServerDataGridModel;

	flatModel: RaGridFlatModel;

	constructor(private entityServiceFabric: EntityServiceFabric,
		private router: Router,
		private route: ActivatedRoute,
		private gridModelComposer: GridModelComposer) {

	}

	ngOnInit() {
		this.entityService = this.entityServiceFabric.getService(this.model.entitiesName);
		this.dataSource$ = this.entityService.entities;
		this.totalLength$ = this.entityService.totalLength;
		this.isLoading$ = this.entityService.isLoading;
		this.flatModel = this.gridModelComposer.toFlatModel(this.model);

		if (this.model.showAddButton) {
			//this.displayedColumns.splice(0, 0, "addButton");
		}


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

	onFilterChanged($event: FilterData) {
		this.currentFilter = $event;
		this.currentPage = 1;
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

		if (this.currentFilter && this.currentFilter.statements.length>0) {
			for (let i = 0; i < this.currentFilter.statements.length; i++) {
				let statement: FilterStatement = this.currentFilter.statements[i];

				qConfig.addFilter(statement.field,statement.value,statement.comparison,statement.logicalOperator);
			}
		}

		this.entityService.getWithQuery(qConfig.toQueryParams());
	}

	selectRow(row: any) {
		this.router.navigate([row.id], { relativeTo: this.route });
	}

	addNew() {
		this.router.navigate(['new'], { relativeTo: this.route });
	}
}
