import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { RaFormLayout, flexExpressions } from "../../../ui-common/ui-common.module";

@Component({
  selector: 'autocomplete-item-edit',
  template: `
    <p>
      autocomplete-item id: {{id$|async}}

  <ra-form [layout]="layout"></ra-form>
    </p>
  `,
  styles: []
})
export class AutocompleteItemEditComponent implements OnInit {
  id$: Observable<string>;
  layout: RaFormLayout;
  constructor(private route: ActivatedRoute) {
    this.id$ = route.paramMap.pipe(map(p => p.get("id")));
    this.layout = {
      rows: [
        { content: { kind: 'caption', title: "Общие поля" } },

        {
          content: {
            kind: 'controls',
            items: [
              {
                flexExpression: flexExpressions.threeItemsExpressions,
                control: {
                  id: "title",
                  kind: 'textbox',
                  label: 'Название',
                  hint: "Название пункта автодополнения",
                  valueKind: 'string',
                  validators: {
                    required: true
                  }
                }
              },
              {
                flexExpression: flexExpressions.threeItemsExpressions,
                control: {
                  id: "title2",
                  kind: 'textbox',
                  label: 'Название2',
                  hint: "Название2 пункта автодополнения",
                  valueKind: 'string',
                  validators: {
                    required: true
                  }
                }
              },
              {
                flexExpression: flexExpressions.threeItemsExpressions,
                control: {
                  id: "title3",
                  kind: 'textbox',
                  label: 'Название3',
                  hint: "Название3 пункта автодополнения",
                  valueKind: 'string',
                  validators: {
                    required: true
                  }
                }
              }
            ]
          }
        },
        { content: { kind: 'divider' } },
        {
          content: {
            kind: 'controls',
            items: [
              {
                flexExpression: flexExpressions.twoItemsExpressions,
                control: {
                  id: "title3",
                  kind: 'textbox',
                  label: 'Название3',
                  hint: "Название4 пункта автодополнения",
                  valueKind: 'string',
                  validators: {
                    required: true
                  }
                }
              },
              {
                flexExpression: flexExpressions.twoItemsExpressions,
                control: {
                  id: "title4",
                  kind: 'textbox',
                  label: 'Название4',
                  hint: "Название4 пункта автодополнения",
                  valueKind: 'string',
                  validators: {
                    required: true
                  }
                }
              },
            ]
          }
        }
      ]
    };

  }

  ngOnInit() {
  }

}
