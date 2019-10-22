import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions} from "../../../ra-schema/ra-schema.module";

@Component({
  selector: 'autocomplete-item-edit',
  template: `<ra-entity-edit [model]="model"></ra-entity-edit>`,
  styles: []
})
export class AutocompleteItemEditComponent implements OnInit {
  model: RaEntityEdit;

  constructor() {
    this.model = {
      entitiesName: "autocompleteItems",
		  title: "Пункт автодополнения",
		  removeDialog: { valueId:"title"},
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
