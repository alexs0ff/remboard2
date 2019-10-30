import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'branch-edit',
	template: `<ra-entity-edit entitiesName="branches"></ra-entity-edit>`,
	
	styles: []
})
export class BranchEditComponent implements OnInit {

	constructor(entityEditSchemaServiceFactory: EntityEditSchemaServiceFactory) {
		const model: RaEntityEdit = {
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

		entityEditSchemaServiceFactory.getService("branches").updateModel(model, ['mainGroup']);

	}

	ngOnInit() {
	}

}
