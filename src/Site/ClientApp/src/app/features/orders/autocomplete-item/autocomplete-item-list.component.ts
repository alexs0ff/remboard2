import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AutocompleteItem } from "./autocomplete-item.models";
import { EntityServiceFabric, IEntityService } from "../../ra-cruds/ra-cruds.module";
import { QueryParamsConfigurator } from "../../ra-cruds/ra-cruds.utils";
import { PageEvent } from '@angular/material/paginator';
import { Sort } from '@angular/material/sort';
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
      showAddButton:true
    };
  }

  ngOnInit() {
    
  }

  test() {
    
  }
}
