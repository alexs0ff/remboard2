import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";
import { EntitySchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'order-type-list',
	template: `
 
<ra-serverdata-grid entitiesName="orderTypes"></ra-serverdata-grid>
  `,
	styles: []
})
export class OrderTypeListComponent implements OnInit {

	dataGrid: RaServerDataGridModel;

	constructor(entitySchemaServiceFactory: EntitySchemaServiceFactory) {
		const dataGrid: RaServerDataGridModel = {
			entitiesName: "orderTypes",
			columns: [
				{ id: "title", name: "Название", options: { canOrder: true, valueKind: 'string' } },
			],
			pageSize: 10,
			panel: { showAddButton: true,},
			filter: null
		};

		entitySchemaServiceFactory.getService("orderTypes").updateModel(dataGrid);
	}

	ngOnInit() {

	}
}
