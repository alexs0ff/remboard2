import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";
import { EntitySchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'order-status-list',
	template: `
 
<ra-serverdata-grid remoteSchemaEntitiesName="orderStatuses"></ra-serverdata-grid>
  `,
	styles: []
})
export class OrderStatusListComponent implements OnInit {

	constructor(entitySchemaServiceFactory: EntitySchemaServiceFactory) {
		const dataGrid: RaServerDataGridModel = {
			entitiesName: "orderStatuses",
			columns: [
				{ id: "title", name: "Название", options: { canOrder: true, valueKind: 'string' } },
				{ id: "orderStatusKindTitle", name: "Тип", options: { canOrder: true, valueKind: 'string' } },
			],
			pageSize: 10,
			panel: { showAddButton: true },
			filter: null
		};

		entitySchemaServiceFactory.getService("orderStatuses").updateModel(dataGrid);
	}

	ngOnInit() {

	}
}
