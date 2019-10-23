import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";

@Component({
	selector: 'branch-edit',
	template: `<ra-entity-edit [model]="model"></ra-entity-edit>`,
	styles: []
})
export class BranchEditComponent implements OnInit {
	model: RaEntityEdit;

	constructor() {
		this.model = {
			entitiesName: "branches",
			title: "Филиалы",
			removeDialog: { valueId: "title" },
			layouts: {
				"MainGroup": {
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

}
