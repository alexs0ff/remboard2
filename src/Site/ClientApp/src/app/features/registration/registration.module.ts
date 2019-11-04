import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UniqueUserLoginAsyncValidator } from './unique-user-login.validator';
import { passwordFormatValidator } from "./password-format.validator";

@NgModule({
	declarations: [],
	imports: [
		CommonModule
	],
	exports: [],
	entryComponents: [],
	providers: []
})
class RegistrationModule { }

export { RegistrationModule, UniqueUserLoginAsyncValidator, passwordFormatValidator};
