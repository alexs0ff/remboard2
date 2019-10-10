import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ui-common/ui-common.module";

@Component({
  selector: 'order-status-list',
  template: `
 
<ra-serverdata-grid [model]="dataGrid"></ra-serverdata-grid>
  `,
  styles: []
})
export class OrderStatusListComponent implements OnInit {

  dataGrid:RaServerDataGridModel;

  constructor() {
    this.dataGrid = {
      entitiesName: "orderStatuses",
      columns: [
		  { canOrder: true, id: "title", name: "Название" },
          { canOrder: true, id: "orderStatusKindTitle", name: "Тип" },
      ],
      pageSize: 10,
		showAddButton: true,
      filter: null
    };
  }

  ngOnInit() {
    
 }
}
