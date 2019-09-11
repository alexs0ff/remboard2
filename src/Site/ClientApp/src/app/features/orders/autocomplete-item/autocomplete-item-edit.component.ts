import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { RaBaseControl, RaTextbox } from "../../../ui-common/ui-common.module";

@Component({
  selector: 'autocomplete-item-edit',
  template: `
    <p>
      autocomplete-item id: {{id$|async}}

  <ra-form [controls]="autocompliteControls"></ra-form>
    </p>
  `,
  styles: []
})
export class AutocompleteItemEditComponent implements OnInit {
  id$: Observable<string>;
  autocompliteControls: Array<RaBaseControl>;
  constructor(private route: ActivatedRoute) {
    this.id$ = route.paramMap.pipe(map(p => p.get("id")));
    this.autocompliteControls = new Array<RaBaseControl>();
    this.autocompliteControls.push(new RaTextbox({ key: "Title", label:"Название"}));

  }

  ngOnInit() {
  }

}
