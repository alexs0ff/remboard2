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
		/*this.dataGrid = {
			entitiesName: "orderTypes",
			columns: [
				{ kind: 'content', canOrder: true, id: "title", name: "Название" },
			],
			pageSize: 10,
			showAddButton: true,
			filter: null
		};*/
	}

	ngOnInit() {

	}
}
