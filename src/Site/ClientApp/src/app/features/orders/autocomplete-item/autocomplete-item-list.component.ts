import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";

@Component({
	selector: 'autocomplete-item-list',
	template: `
 
<ra-serverdata-grid [model]="dataGrid" remoteSchemaEntitiesName="autocompleteItems"></ra-serverdata-grid>
  `,
	styles: []
})
export class AutocompleteItemListComponent implements OnInit {

	dataGrid: RaServerDataGridModel;

	constructor() {
	}

	ngOnInit() {

	}
}
