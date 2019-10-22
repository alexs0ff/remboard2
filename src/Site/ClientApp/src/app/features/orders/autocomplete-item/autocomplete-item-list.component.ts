import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";

@Component({
	selector: 'autocomplete-item-list',
	template: `
 
<ra-serverdata-grid [model]="dataGrid"></ra-serverdata-grid>
  `,
	styles: []
})
export class AutocompleteItemListComponent implements OnInit {

	dataGrid: RaServerDataGridModel;

	constructor() {
		this.dataGrid = {
			entitiesName: "autocompleteItems",
			columns: [
				{ id: "title", name: "Название", options:{canOrder:true,valueKind:'string'}},
				{ id: "autocompleteKindTitle", name: "Тип", options: { canOrder: true, valueKind: 'string' } },
			],
			pageSize: 50,
			panel: { showAddButton: true,},
			filter: {
				columns: [
					{
						id: "title",
						kind: 'textbox',
						label: "Название",
						valueKind: "string",
						validators: { required: true }
					},
					{
						kind: 'selectbox',
						id: 'autocompleteKindId',
						label: 'Тип автодополнения',
						hint: 'Тип автодополнения',
						validators: { required: true },
						valueKind: 'number',
						source: {
							kind: 'items',
							items: [
								{ key: 1, value: "Бренд" },
								{ key: 2, value: "Комплектация" },
								{ key: 3, value: "Внешний вид" }
							]
						}
					}
				]
			}
		};
	}

	ngOnInit() {

	}
}
