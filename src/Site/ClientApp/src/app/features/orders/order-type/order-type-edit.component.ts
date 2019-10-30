import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'order-type-edit',
	template: `<ra-entity-edit entitiesName="orderTypes"></ra-entity-edit>`,
	styles: []
})
export class OrderTypeEditComponent implements OnInit {
	model: RaEntityEdit;

	constructor(entityEditSchemaServiceFactory: EntityEditSchemaServiceFactory) {
		const model: RaEntityEdit = {
			entitiesName: "orderTypes",
			title: "Статус заказа",
			removeDialog: { valueId: "title" },
			layouts: {
				"mainGroup": {
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
			}
		};
		entityEditSchemaServiceFactory.getService("orderTypes").updateModel(model, ['mainGroup']);
	}

	ngOnInit() {
	}

}
