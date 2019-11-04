import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Dictionary } from "../../app.models";

@Injectable()
export class FormErrorService {

  private readonly errors: Dictionary<string> = {
    "required": "Нужно задать или ввести значение",
    "email": "Не корректный email",
    "uniqueUserLogin": "Пользователь с таким именем уже существует",
    "uniqueUserEmail": "Пользователь с таким email уже существует",
    "userLoginFormat": "Ошибочный формат логина пользователя",
    "passwordMismatch": "Пароли должны совпадать",
    "passwordFormat": "Пароль должен содержать латинские буквы разного регистра, цифры и быть длиной не менее 8 символов",
  };

  private readonly default: string = "Общая ошибка";

	getErrorMessage(form: FormGroup, controlName: string):string {
    let error:string  = this.default;
    for (let errorName in this.errors) {
      if (form.controls[controlName].hasError(errorName)) {
        error = this.errors[errorName];
        break;
      }
    }
    return error;
  }
}
