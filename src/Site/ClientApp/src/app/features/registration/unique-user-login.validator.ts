import { Injectable} from "@angular/core";
import { AsyncValidator } from "@angular/forms";
import { AbstractControl } from "@angular/forms/forms";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { LoginInfo } from "./registration.models";

@Injectable({ providedIn: 'root' })
export class UniqueUserLoginAsyncValidator implements AsyncValidator
{
	constructor(private http: HttpClient) {

	}

	validate(control: AbstractControl): Promise<{ [index: string]: any; }> | Observable<{ [index: string]: any; }> {
		return this.http.get<LoginInfo>("https://localhost:44378/api/login/userinfo/" + control.value).pipe(
			map(li => (li && li.userExists) ? { uniqueUserLogin:true } : null),
			catchError(() => null));
	}
}
