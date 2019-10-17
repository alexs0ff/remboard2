import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ui-common/ui-common.module";

@Component({
	selector: 'order-type-list',
	template: `
 
<ra-serverdata-grid [model]="dataGrid"></ra-serverdata-grid>
  `,
	styles: []
})
export class OrderTypeListComponent implements OnInit {

	dataGrid: RaServerDataGridModel;

	constructor() {
		this.dataGrid = {
			entitiesName: "orderTypes",
			columns: [
				{ id: "title", name: "Название", options: { canOrder: true, valueKind: 'string' } },
			],
			pageSize: 10,
			panel: { showAddButton: true,},
			filter: null
		};
	}

	ngOnInit() {

	}
}
