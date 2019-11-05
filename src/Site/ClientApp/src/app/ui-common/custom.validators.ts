import { ValidatorFn, AbstractControl } from '@angular/forms'

export function notEmptyArrayValidator(): ValidatorFn {
	return (control: AbstractControl): { [key: string]: any } | null => {
		let forbidden: boolean = true;

		if (control.value && control.value.length) {
			forbidden = false;
		}

		return forbidden ? { 'required': true } : null;
	};
}

export function uniqueArrayValidator(keyColumn:string): ValidatorFn {
	return (control: AbstractControl): { [key: string]: any } | null => {
		let unique: boolean = true;

		if (control.value && control.value.length) {
			var hashSet = {};
			for (var i = 0; i < control.value.length; i++) {
				const item = control.value[i];

				if (!item) {
					continue;
				}

				const keyValue = item[keyColumn];

				if (hashSet[keyValue]) {
					unique = false;
					break;
				}

				hashSet[keyValue] = true;
			}

		}

		return unique ? null : { 'uniqueArray': true };
	};
}

export function matchToControlValidator(toControlId: string, errorMessage:string): ValidatorFn {
	return (control: AbstractControl): { [key: string]: any } | null => {
		let hasError: boolean = true;

		if (control.value && control.parent && control.parent.controls[toControlId]) {

			hasError = control.value !== control.parent.controls[toControlId].value;
		}

		const errorObject = {};

		errorObject[errorMessage] = true;

		return hasError ? errorObject: null;
	};
}
