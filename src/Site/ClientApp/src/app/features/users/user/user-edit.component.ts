import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'user-edit',
	template: `<ra-entity-edit entitiesName="users"></ra-entity-edit>`,
	
	styles: []
})
export class UserEditComponent implements OnInit {
	
	constructor(entityEditSchemaServiceFactory: EntityEditSchemaServiceFactory) {
		const model: RaEntityEdit = {
			entitiesName: "users",
			title: "Пользователи",
			removeDialog: { valueId: "title" },
			layouts: {
				"mainGroup": {
					rows: [
						{ content: { kind: 'hidden', items: ['id', 'projectRoleTitle','branches'] } },
						{
							content: {
								kind: 'controls',
								items: [
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											kind: 'selectbox',
											id: 'projectRoleId',
											label: 'Роль',
											hint: 'Роль в проекте',
											validators: { required: true },
											valueKind: 'number',
											source: {
												kind: 'items',
												items: [
													{ key: 1, value: "Администратор" },
													{ key: 2, value: "Менеджер" },
													{ key: 3, value: "Инженер" }
												]
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "loginName",
											kind: 'textbox',
											label: 'Логин',
											hint: "Логин пользователя",
											valueKind: 'string',
											validators: {
												required: true
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "firstName",
											kind: 'textbox',
											label: 'Имя',
											hint: "Имя пользователя",
											valueKind: 'string',
											validators: {
												required: true
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "lastName",
											kind: 'textbox',
											label: 'Фамилия',
											hint: "Фамилия пользователя",
											valueKind: 'string',
											validators: {
												required: true
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "middleName",
											kind: 'textbox',
											label: 'Отчество',
											hint: "Отчество пользователя",
											valueKind: 'string',
											validators: {
												required: false
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "phone",
											kind: 'textbox',
											label: 'Телефон',
											hint: "Телефон пользователя",
											valueKind: 'string',
											validators: {
												required: true
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "email",
											kind: 'textbox',
											label: 'Почта',
											hint: "Эл почта пользователя",
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

		entityEditSchemaServiceFactory.getService("users").updateModel(model, ['mainGroup']);

	}

	ngOnInit() {
	}

}
