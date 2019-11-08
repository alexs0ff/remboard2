import { Component, OnInit } from '@angular/core';
import { RaEntityEdit, flexExpressions, RaMultiselect, RaFormLayout, RaTextBox, RaFormLayoutItem } from "../../../ra-schema/ra-schema.module";
import { EntityEditSchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";
import { SchemaFetchEvent, matchToControlValidator } from "../../../ui-common/ui-common.module";
import { ExtensionParts } from "../../../ui-common/forms-composition/forms-composition.models";
import { UniqueUserLoginAsyncValidator, passwordFormatValidator } from "../../registration/registration.module";
import { UserLoginValidator } from "../../registration/user-login.validator";
import emailMask from 'text-mask-addons/dist/emailMask'

@Component({
	selector: 'user-edit',
	template: `<ra-entity-edit entitiesName="users" (schemaFetch)="onSchemaFetch($event);" [extensionParts]="extensions"></ra-entity-edit>`,
	
	styles: []
})
export class UserEditComponent implements OnInit {

	extensions: ExtensionParts;

	constructor(uniqueUserLoginAsyncValidator: UniqueUserLoginAsyncValidator, userLoginValidator: UserLoginValidator) {
	
		this.extensions = {
			asyncValidators: {
				 "uniqueUserLogin": uniqueUserLoginAsyncValidator.validate.bind(uniqueUserLoginAsyncValidator),
			},
			validators: {
				"userLogin": userLoginValidator.validate.bind(userLoginValidator),
				"passwordMatch": matchToControlValidator("password","passwordMismatch"),
				"passwordFormat": passwordFormatValidator("passwordFormat"),
			},
			controlMasks: {
				masks: {
					"email": emailMask,
					"phone": ['+', /\d/,' ','(',/[1-9]/,/\d/,/\d/,')',' ',/\d/,/\d/,/\d/,'-',/\d/,/\d/,/\d/,/\d/]
				}
			}
		}
	}

	ngOnInit() {
	}

	onSchemaFetch(event: SchemaFetchEvent) {
		let layouts = ['updateGroup', 'mainGroup'];

		if (event.isNewEntity) {
			layouts = ['createGroup', 'mainGroup'];
		}

		event.customSchema = {
			editForm: this.buildEditForm(event.isNewEntity),
			layouts: layouts
		};
	}


	private buildEditForm(isNewEntity: boolean): RaEntityEdit {
		const userLoginControl: RaFormLayoutItem = {
			flexExpression: flexExpressions.oneItemExpressions,
			control: {
				id: "loginName",
				kind: 'textbox',
				label: 'Логин',
				hint: "Логин пользователя",
				valueKind: 'string',
				validators: {
					required: true,
					asyncValidators: ['uniqueUserLogin'],
					validators: ['userLogin'],
					maxLength:50,
				},
				updateOn: 'blur',
				disabled: !isNewEntity
			}
		};

		const createLayout: RaFormLayout = {
			rows: [
				{
					content: {
						kind: 'controls',
						items: [
							userLoginControl,
							{
								flexExpression: flexExpressions.twoItemsExpressions,
								control: {
									id: "password",
									kind: 'password',
									label: 'Пароль',
									hint: "Пароль пользователя",
									valueKind: 'string',
									validators: {
										required: true,
										validators: ["passwordFormat"]
									}
								},

							},
							{
								flexExpression: flexExpressions.twoItemsExpressions,
								control: {
									id: "passwordSecond",
									kind: 'password',
									label: 'Пароль',
									hint: "Повторите пароль",
									valueKind: 'string',
									validators: {
										required: true,
										validators: ['passwordMatch']
									}
								},

							}
						]
					}
				}
			]
		};

		const updateLayout: RaFormLayout = {
			rows: [
				{
					content: {
						kind: 'controls',
						items: [
							userLoginControl
						]
					}
				}
			]
		};

		const model: RaEntityEdit = {
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
										flexExpression: flexExpressions.oneItemExpressions,
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
											kind: 'maskbox',
											label: 'Телефон',
											hint: "Телефон пользователя",
											valueKind: 'string',
											floatLabel:'always',
											validators: {
												required: true
											},
											textMask: {
												maskId: 'phone',
												conformToMask: true
											}
										}
									},
									{
										flexExpression: flexExpressions.twoItemsExpressions,
										control: {
											id: "email",
											kind: 'maskbox',
											label: 'Почта',
											hint: "Эл почта пользователя",
											floatLabel: 'auto',
											valueKind: 'string',
											validators: {
												required: true,
												email: true
											},
											textMask: {
												maskId: 'email',
												conformToMask:false
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
											keyColumn: 'branchId',
											validators: {
												required: false,
												unique:true,
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
		
		if (isNewEntity) {
			model.layouts["createGroup"] = createLayout;
		} else {
			model.layouts["updateGroup"] = updateLayout;
		}

		return model;
	}
}
