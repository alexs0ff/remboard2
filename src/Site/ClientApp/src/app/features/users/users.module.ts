import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './user/user-list.component';
import { CrudsEntityMetadata, CrudEntityConfigurator, RaCrudsModule, EntitySchemaConfigurator, EntitySchemaMetadata, EntityEditSchemaMetadata, EntityEditSchemaConfigurator } from "../ra-cruds/ra-cruds.module";
import { UserEntity } from "./user/user.models";
import { UiCommonModule } from "../../ui-common/ui-common.module";
import { UserEditComponent } from "./user/user-edit.component";

const routes: Routes = [
	{ path: 'user', component: UserListComponent },
	{ path: 'user/:id', component: UserEditComponent },
	
];

const config: CrudsEntityMetadata = {
	"users": new CrudEntityConfigurator<UserEntity>("user"),
}

const configSchema: EntitySchemaMetadata = {
	"users": new EntitySchemaConfigurator<UserEntity>("user"),
}

const configEditSchema: EntityEditSchemaMetadata = {
	"users": new EntityEditSchemaConfigurator<UserEntity>("user")
}
@NgModule({
	declarations: [
		UserListComponent,
		UserEditComponent
	],
	imports: [
		CommonModule,
		UiCommonModule,
		RouterModule.forChild(routes),
		RaCrudsModule.forCrudsFeature("users", config),
		RaCrudsModule.forSchemaFeature("usersSchema", configSchema),
		RaCrudsModule.forEditSchemaFeature("usersEditSchema", configEditSchema),
	],
	providers: []
})
export class UsersModule {
	constructor() {

	}

}
