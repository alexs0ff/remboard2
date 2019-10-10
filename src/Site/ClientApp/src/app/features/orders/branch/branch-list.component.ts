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
		{ canOrder: true, id: "title", name: "Название" },
        { canOrder: true, id: "legalName", name: "Юр наименование" },
        { canOrder: true, id: "address", name: "Адрес" },
      ],
      pageSize: 10,
		showAddButton: true,
      filter: null
    };
  }

  ngOnInit() {

  }
}
