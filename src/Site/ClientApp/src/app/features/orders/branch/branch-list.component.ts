import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ui-common/ui-common.module";

@Component({
	selector: 'branch-list',
	template: `
 
<ra-serverdata-grid [model]="dataGrid"></ra-serverdata-grid>
  `,
	styles: []
})
export class BranchListComponent implements OnInit {

	dataGrid: RaServerDataGridModel;

	constructor() {
		this.dataGrid = {
			entitiesName: "branches",
			columns: [
				{
					id: 'totalHeader',
					name: 'Общий заголовок',
					columns: [
						{
							id: "leftPart",
							name: "Левая часть",
							columns: [
								{ id: "title", name: "Название", options: { canOrder: true } },
								{ id: "legalName", name: "Юр наименование", options: { canOrder: true } },
							]
						}, {
							id: "rightPart",
							name: "Правая часть",
							columns: [{ id: "address", name: "Адрес", options: { canOrder: true } }]
						}
					]
				}
			],
			pageSize: 10,
			showAddButton: true,
			filter: null
		};
	}

	ngOnInit() {

	}
}
