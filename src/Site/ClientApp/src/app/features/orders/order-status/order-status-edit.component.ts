import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";
import { SchemaFetchEvent } from "../../../ui-common/ui-common.module";

@Component({
	selector: 'order-status-edit',
	template: `<ra-entity-edit entitiesName="orderStatuses" (schemaFetch)="onSchemaFetch($event);"></ra-entity-edit>`,
	
	styles: []
})
export class OrderStatusEditComponent implements OnInit {
	private model: RaEntityEdit;
	constructor() {
		this.model = {
			entitiesName: "orderStatuses",
			title: "Статус заказа",
			removeDialog: { valueId: "title" },
			layouts: {
				"mainGroup": {
					rows: [
						{ content: { kind: 'hidden', items: ['id', 'orderStatusKindTitle'] } },
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
											hint: "Название статуса",
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
											id: 'orderStatusKindId',
											label: 'Тип статуса',
											hint: 'Тип статуса',
											validators: { required: true },
											valueKind: 'number',
											source: {
												kind: 'items',
												items: [
													{ key: 1, value: "Новые" },
													{ key: 2, value: "На исполнении" },
													{ key: 3, value: "Отложенные" },
													{ key: 4, value: "Исполненные" },
													{ key: 5, value: "Закрытые" }
												]
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
