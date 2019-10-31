import { Component, OnInit } from '@angular/core';
import { RaServerDataGridModel } from "../../../ra-schema/ra-schema.module";
import { EntitySchemaServiceFactory } from "../../ra-cruds/ra-cruds.module";

@Component({
	selector: 'user-list',
	template: `
 
<ra-serverdata-grid entitiesName="users"></ra-serverdata-grid>
  `,
	styles: []
})
export class UserListComponent implements OnInit {

	
	constructor(entitySchemaServiceFactory: EntitySchemaServiceFactory) {
		const dataGrid: RaServerDataGridModel = {
			entitiesName: "users",
			columns: [
				{ id: "projectRoleTitle", name: "Роль в проекте", options: { canOrder: true, valueKind:'string' } },
				{ id: "loginName", name: "Логин", options: { canOrder: true, valueKind: 'string' } },
				{ id: "firstName", name: "Имя", options: { canOrder: true, valueKind: 'string' } },
				{ id: "lastName", name: "Фамилия", options: { canOrder: true, valueKind: 'string' } },
				{ id: "middleName", name: "Отчество", options: { canOrder: true, valueKind: 'string' } },
				{ id: "phone", name: "Телефон", options: { canOrder: true, valueKind: 'string' } },
			],
			panel: {showAddButton: true,},
			filter: {
				columns: [
					{
						id: "loginName",
						kind: 'textbox',
						label: "Логин",
						valueKind: "string",
						validators: { required: true }
					},
					{
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
					},
					
				]
			}
		};

		entitySchemaServiceFactory.getService("users").updateModel(dataGrid);
	}

	ngOnInit() {

	}
}
