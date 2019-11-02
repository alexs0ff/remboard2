import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";
import { SchemaFetchEvent } from "../../../ui-common/ui-common.module";


@Component({
	selector: 'branch-edit',
	template: `<ra-entity-edit entitiesName="branches" (schemaFetch)="onSchemaFetch($event);"></ra-entity-edit>`,
	
	styles: []
})
export class BranchEditComponent implements OnInit {
	private model: RaEntityEdit;
	constructor() {
		this.model= {
			entitiesName: "branches",
			title: "Филиалы",
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
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "title",
											kind: 'textbox',
											label: 'Название',
											hint: "Название филиала",
											valueKind: 'string',
											validators: {
												required: true
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "legalName",
											kind: 'textbox',
											label: 'Юр название',
											hint: "Юридическое название филиала",
											valueKind: 'string',
											validators: {
												required: true
											}
										}
									},
									{
										flexExpression: flexExpressions.oneItemExpressions,
										control: {
											id: "address",
											kind: 'textarea',
											label: 'Адрес',
											hint: "Адрес филиала",
											valueKind: 'string',
											validators: {
												required: false
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
