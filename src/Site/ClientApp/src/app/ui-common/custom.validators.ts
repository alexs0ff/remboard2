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
