import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions} from "../../../ra-schema/ra-schema.module";

@Component({
  selector: 'autocomplete-item-edit',
	template: `<ra-entity-edit entitiesName="autocompleteItems"></ra-entity-edit>`,
  styles: []
})
export class AutocompleteItemEditComponent implements OnInit {
  
constructor() {

}

ngOnInit() {
  }

}
