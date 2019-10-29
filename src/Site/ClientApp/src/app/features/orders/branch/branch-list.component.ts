import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";
import { EntitySchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'branch-list',
	template: `
 
<ra-serverdata-grid remoteSchemaEntitiesName="branches"></ra-serverdata-grid>
  `,
	styles: []
})
export class BranchListComponent implements OnInit {

	
	constructor(entitySchemaServiceFactory: EntitySchemaServiceFactory) {
		const dataGrid: RaServerDataGridModel = {
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
				{ id: "title", name: "Название", options: { canOrder: true, valueKind:'string' } },
				{ id: "legalName", name: "Юр наименование", options: { canOrder: true, valueKind: 'string' } },
				{ id: "address", name: "Адрес", options: { canOrder: true, valueKind: 'string' } }
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

		entitySchemaServiceFactory.getService("branches").updateModel(dataGrid);
	}

	ngOnInit() {

	}
}
