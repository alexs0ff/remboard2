import { Component, OnInit,Input } from '@angular/core';
import { EntityServiceFactory, IEntityService, IEntitySchemaService } from "../../../features/ra-cruds/ra-cruds.module";
import { Observable } from 'rxjs';
import { AutocompleteItem } from "../../../features/orders/autocomplete-item/autocomplete-item.models";
import { QueryParamsConfigurator } from "../../../features/ra-cruds/ra-cruds.utils";
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
import { Router, ActivatedRoute } from '@angular/router'
import { FilterData,FilterStatement } from "../../ra-filter.models";
import { GridModelComposer } from "./ra-serverdata-grid.services";
import { RaServerDataGridModel, RaGridFlatModel } from "../../../ra-schema/ra-schema.module";
import { EntitySchemaServiceFactory } from "../../../features/ra-cruds/ra-schema-cruds.services";

@Component({
	selector: 'ra-serverdata-grid',
	templateUrl: './ra-serverdata-grid.component.html',
	styleUrls: ['./ra-serverdata-grid.component.scss']
})
export class RaServerdataGridComponent implements OnInit {

	dataSource$: Observable<AutocompleteItem[]>;
	totalLength$: Observable<number>;
	isLoading$: Observable<boolean>;

	pageSize = 50;

	private currentPage: number = 1;
	private sortedColumn: string = "";
	private sortDirection: string = "";
	private entityService: IEntityService<any>;
	private entitySchemaService: IEntitySchemaService<any>;
	private currentFilter: FilterData = null;

	@Input()
	model: RaServerDataGridModel;

	flatModel: RaGridFlatModel;

	constructor(private entityServiceFabric: EntityServiceFactory,private entitySchemaServiceFactory: EntitySchemaServiceFactory,
		private router: Router,
		private route: ActivatedRoute,
		private gridModelComposer: GridModelComposer) {

	}

	ngOnInit() {
		this.entityService = this.entityServiceFabric.getService(this.model.entitiesName);
		this.entitySchemaService = this.entitySchemaServiceFactory.getService(this.model.entitiesName);
		this.dataSource$ = this.entityService.entities;
		this.totalLength$ = this.entityService.totalLength;
		this.isLoading$ = this.entityService.isLoading;
		this.flatModel = this.gridModelComposer.toFlatModel(this.model);

		if (this.model.panel) {
			this.flatModel.displayedColumns.splice(0, 0, "gridControlPanel");
			this.flatModel.headers[0].splice(0, 0, "gridControlPanel");

			for (let i = 1; i < this.flatModel.headers.length; i++) {
				this.flatModel.headers[i].splice(0, 0, "gridHiddenColumn");
			}

		}


		if (this.model.pageSize) {
			this.pageSize = this.model.pageSize;
		}

		this.refreshData();

		this.entitySchemaService.getWithQuery(null);
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
