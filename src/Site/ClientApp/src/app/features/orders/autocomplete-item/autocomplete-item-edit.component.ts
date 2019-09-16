import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { RaFormLayout, flexExpressions, RaEntityEdit } from "../../../ui-common/ui-common.module";

@Component({
  selector: 'autocomplete-item-edit',
  template: `<ra-entity-edit [model]="model"></ra-entity-edit>`,
  styles: []
})
export class AutocompleteItemEditComponent implements OnInit {
  id$: Observable<string>;
  model: RaEntityEdit;

  constructor(private route: ActivatedRoute) {
    this.id$ = route.paramMap.pipe(map(p => p.get("id")));
    this.model = {
      entitiesName: "autocompleteItems",
      title: "Пункт автодополнения",
      layout: {
        rows: [
          { content: { kind: 'hidden', items: ['id','autocompleteKindTitle']  } },
          {
            content: {
              kind: 'controls',
              items: [
                {
                  flexExpression: flexExpressions.twoItemsExpressions,
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
                  flexExpression: flexExpressions.twoItemsExpressions,
                  control: {
                    kind: 'selectbox',
                    id: 'autocompleteKindId',
                    label: 'Тип автодополнения',
                    hint: 'Тип автодополнения',
                    validators: { required: true },
                    valueKind: 'number',
                    source: {
                      kind: 'items', items: [
                        { key: 1, value: "Бренд"} ,
                        { key: 2, value: "Комплектация"} ,
                        { key: 3, value: "Внешний вид"} 
                      ]
                    }
                  }
                }
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
