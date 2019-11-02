import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";
import { SchemaFetchEvent } from "../../../ui-common/ui-common.module";

@Component({
	selector: 'order-type-edit',
	template: `<ra-entity-edit entitiesName="orderTypes" (schemaFetch)="onSchemaFetch($event);"></ra-entity-edit>`,
	styles: []
})
export class OrderTypeEditComponent implements OnInit {
	model: RaEntityEdit;

	constructor() {
		this.model= {
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
	}

	ngOnInit() {
	}
	onSchemaFetch(event: SchemaFetchEvent) {
		event.customSchema = {
			editForm: this.model,
			layouts: ['mainGroup']
		};
	}

}
