import { Component, OnInit,Input } from '@angular/core';
import { EntityServiceFactory, IEntityService, IEntitySchemaService } from "../../../features/ra-cruds/ra-cruds.module";
import { Observable, of } from 'rxjs';
import { map,tap } from 'rxjs/operators';
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


	private currentPage: number = 1;
	private sortedColumn: string = "";
	private sortDirection: string = "";
	private entityService: IEntityService<any>;
	private entitySchemaService: IEntitySchemaService<any>;
	private currentFilter: FilterData = null;

	flatModel$: Observable<RaGridFlatModel>;

	pageSize:number = 50;

	@Input()
	model: RaServerDataGridModel;

	@Input()
	remoteSchemaEntitiesName:string;

	constructor(private entityServiceFabric: EntityServiceFactory,private entitySchemaServiceFactory: EntitySchemaServiceFactory,
		private router: Router,
		private route: ActivatedRoute,
		private gridModelComposer: GridModelComposer) {

	}

	ngOnInit() {

		let entitiesName = this.remoteSchemaEntitiesName;
		let useModel = false;

		if (this.model && this.model.entitiesName) {
			useModel = true;
			entitiesName = this.model.entitiesName;
		}

		this.entityService = this.entityServiceFabric.getService(entitiesName);
		this.entitySchemaService = this.entitySchemaServiceFactory.getService(entitiesName);
		this.dataSource$ = this.entityService.entities;
		this.totalLength$ = this.entityService.totalLength;
		this.isLoading$ = this.entityService.isLoading;

		let gridModelComposer = this.gridModelComposer;

		if (useModel) {
			let converted = GridModelComposer.convertToFlat(gridModelComposer, this.model);
			this.flatModel$ = of(converted);
		} else {
			this.flatModel$ = this.entitySchemaService.gridModel.pipe(map(m => GridModelComposer.convertToFlat(gridModelComposer, m)));
			
		}

		this.refreshData();

		if (!useModel) {
			this.entitySchemaService.getWithQuery(null);
		}
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
