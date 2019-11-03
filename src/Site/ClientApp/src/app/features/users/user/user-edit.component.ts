import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions, RaMultiselect } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";
import { SchemaFetchEvent } from "../../../ui-common/ui-common.module";
import { ExtensionParts } from "../../../ui-common/forms-composition/forms-composition.models";
import { UniqueUserLoginAsyncValidator } from "../../registration/registration.module";

@Component({
	selector: 'user-edit',
	template: `<ra-entity-edit entitiesName="users" (schemaFetch)="onSchemaFetch($event);" [extensionParts]="extensions"></ra-entity-edit>`,
	
	styles: []
})
export class UserEditComponent implements OnInit {
	private model: RaEntityEdit;

	extensions: ExtensionParts;

	constructor(uniqueUserLoginAsyncValidator: UniqueUserLoginAsyncValidator) {

		this.extensions = {
			asyncValidators: { "uniqueUserLogin": uniqueUserLoginAsyncValidator.validate.bind(uniqueUserLoginAsyncValidator)}
		}

		this.model= {
			entitiesName: "users",
			title: "Пользователи",
			removeDialog: { valueId: "title" },
			layouts: {
				"mainGroup": {
					rows: [
						{ content: { kind: 'hidden', items: ['id', 'projectRoleTitle'] } },
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
												required: true,
												asyncValidators: ['uniqueUserLogin'],
											},
											updateOn:'blur',
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
												required: true,
												email:true
											}
										}
									},
									{
										flexExpression: flexExpressions.oneItemExpressions,
										control: {
											id: 'branches',
											kind: 'multiselect',
											label: 'Филиалы',
											hint: 'Филиалы пользователя',
											displayColumns: ['branchTitle'],
											validators: {
												required: false,
											},
											source: {
												kind: 'remote',
												url: 'api/branches',
												filterColumns: ['title'],
												remoteMapping: {
													'id': 'branchId',
													'title': 'branchTitle',
												},
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
