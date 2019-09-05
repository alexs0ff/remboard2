import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'autocomplete-item-edit',
  template: `
    <p>
      autocomplete-item id: {{id$|async}}
    </p>
  `,
  styles: []
})
export class AutocompleteItemEditComponent implements OnInit {
  id$:Observable<string>;
  constructor(private route: ActivatedRoute) {
    this.id$ = route.paramMap.pipe(map(p => p.get("id")));
  }

  ngOnInit() {
  }

}
