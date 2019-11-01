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
