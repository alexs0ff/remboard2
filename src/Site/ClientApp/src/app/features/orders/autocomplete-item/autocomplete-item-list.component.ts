import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ui-common/ui-common.module";

@Component({
  selector: 'autocomplete-item-list',
  template: `
 
<ra-serverdata-grid [model]="dataGrid"></ra-serverdata-grid>
  `,
  styles: []
})
export class AutocompleteItemListComponent implements OnInit {

  dataGrid:RaServerDataGridModel;

  constructor() {
    this.dataGrid = {
      entitiesName: "autocompleteItems",
      columns: [
        { canOrder: true, id: "title", name: "Название" },
        { canOrder: false, id: "autocompleteKindTitle", name: "Тип" },
      ],
      pageSize: 10,
		  showAddButton: true,
		  filter: { columns: [{kind:"text",id:"title",title:"название"}] }
    };
  }

  ngOnInit() {
    
  }

  test() {
    
  }
}
