import { Directive, forwardRef, Injectable } from '@angular/core';
import {
  AsyncValidator,
  AbstractControl,
  NG_ASYNC_VALIDATORS,
  ValidationErrors
} from '@angular/forms';
import { Store, select } from "@ngrx/store";
import { catchError, map, tap, take, distinctUntilChanged, takeUntil, takeWhile, pairwise, withLatestFrom,filter} from 'rxjs/operators';
import { Observable, of, timer } from 'rxjs';
import { selectLoginMessage } from "../auth.selectors";

//async validator from here https://stackblitz.com/angular/xneneqqagjy
@Injectable()
export class LoginErrorValidator implements AsyncValidator {

  constructor(private store: Store<{}>) {}

  validate(control: AbstractControl): Promise<{ [index: string]: any; }> | Observable<{ [index: string]: any; }> | null {
    
    //return of(control.value).pipe(map(message => (message != null && message.length > 2 ? { "loginError": true } : {})),tap(m => { console.log("ddd", m); }));

    const exp:Observable<string> = this.store.pipe(select(selectLoginMessage));
    return timer(500, 500).pipe(withLatestFrom(exp),
      map(
        ([first, second]) => second), pairwise(), filter(i=>i[0]!==i[1] && i[1]!=null),take(1),tap(m => { console.log("1", m); }),
      map(message => (message != null ? { "loginError": true } : {})),
      tap(m => { console.log("ddd", m); }));

    //return this.store.pipe(select(selectLoginMessage), pairwise(), tap(m => { console.log("1", m); }), map(message => (message != null ? { "loginError": true } : {})), tap(m => { console.log("ddd", m); }));
    //return this.store.pipe(select(selectLoginMessage), takeUntil(this.store.pipe(select(selectLoginMessage),take(1))), map(message => (message != null ? { "loginError": true } : {})), tap(m => { console.log("ddd", m); }));
    //return this.store.pipe(select(selectLoginMessage), take(1), map(message => (message != null ? { "loginError": true } : {})), tap(m => { console.log("ddd", m); }));
    //return this.store.pipe(select(selectLoginMessage), map(message => (message != null ? { "loginError": true } : {})), tap(m => { console.log("ddd", m); }));


  }
}

//directive uses only for template driven component
@Directive({
  selector: '[loginFormError]',
  providers: [
    {
      provide: NG_ASYNC_VALIDATORS,
      useExisting: forwardRef(() => LoginErrorValidator),
      multi: true
    }
  ]
})
export class LoginErrorValidatorDirective {
  constructor(private validator: LoginErrorValidator) { }

  validate(control: AbstractControl) {
    this.validator.validate(control);
  }
}
