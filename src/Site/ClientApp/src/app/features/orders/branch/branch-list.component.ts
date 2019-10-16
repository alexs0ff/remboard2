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
				/*{
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
				}*/
				{ id: "title", name: "Название", options: { canOrder: true } },
				{ id: "legalName", name: "Юр наименование", options: { canOrder: true } },
				{ id: "address", name: "Адрес", options: { canOrder: true } }
			],
			panel: {showAddButton: true,},
			filter: {
				columns: [
					{
						id: "legalName",
						kind: 'textbox',
						label: "Юр название",
						valueKind: "string",
						validators: { required: true }
					},
					{
						id: "title",
						kind: 'textbox',
						label: "Название филиала",
						valueKind: "string",
						validators: { required: true }
					},
					
				]
			}
		};
	}

	ngOnInit() {

	}
}
