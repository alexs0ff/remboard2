import { Component, OnInit } from '@angular/core';
import { flexExpressions, RaEntityEdit } from "../../../ui-common/ui-common.module";

@Component({
  selector: 'order-type-edit',
  template: `<ra-entity-edit [model]="model"></ra-entity-edit>`,
  styles: []
})
export class OrderTypeEditComponent implements OnInit {
  model: RaEntityEdit;

  constructor() {
    this.model = {
      entitiesName: "orderTypes",
      title: "Статус заказа",
      removeDialog: { valueId: "title" },
      layout: {
        rows: [
			  { content: { kind: 'hidden', items: ['id'] } },
          {
            content: {
              kind: 'controls',
              items: [
                {
                  flexExpression: flexExpressions.oneItemExpressions,
                  control: {
                    id: "title",
                    kind: 'textbox',
                    label: 'Название',
                    hint: "Название типа",
                    valueKind: 'string',
                    validators: {
                      required: true
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
