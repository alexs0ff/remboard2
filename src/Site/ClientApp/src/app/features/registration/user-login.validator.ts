import { Validator } from "@angular/forms";

import { Injectable } from "@angular/core";
import { AbstractControl } from "@angular/forms/forms";


@Injectable({ providedIn: 'root' })
export class UserLoginValidator implements Validator {

	private allowedCharacters: string = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+';

	constructor() {

	}

	validate(control: AbstractControl): { [index: string]: any; } {

		if (typeof control.value !== 'string') {
			return { validateUsername: 'The property to validate must be of type \'string\'' };
		}


		for (var i = 0; i < control.value.length; i++) {
			if (this.allowedCharacters.indexOf(control.value[i]) < 0) {
				return { userLoginFormat: control.value[i] }
			}
		}
		return null;
	}
}
