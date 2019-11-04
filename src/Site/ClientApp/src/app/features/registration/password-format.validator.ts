import { ValidatorFn, AbstractControl, Validators } from '@angular/forms'

export function passwordFormatValidator(errorMessage:string): ValidatorFn {
	return (control: AbstractControl): { [key: string]: any } | null => {
		const errorObj = {};
		if (typeof control.value !== 'string') {
			errorObj[errorMessage] = "The property to validate must be of type \'string\''";
			return errorObj;
		}

		const regex: RegExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/;
		errorObj[errorMessage] = true;
		return regex.test(control.value)?null:errorObj;
	};
}
