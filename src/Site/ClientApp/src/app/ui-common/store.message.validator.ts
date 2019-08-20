import { AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable, of, timer } from 'rxjs';
import { map,take, pairwise, withLatestFrom, filter } from 'rxjs/operators';

const interval = 500;
export function storeMessageValidator(expression: Observable<string>, error: { [index: string]: any; }): AsyncValidatorFn {
  return (control: AbstractControl): Promise<{ [index: string]: any; }> | Observable<{ [index: string]: any; }> | null =>
  {
    return timer(interval, interval).pipe(withLatestFrom(expression),
      map(
        ([first, second]) => second), pairwise(), filter(i => i[0] !== i[1] && i[1] != null), take(1),
        map(message => (message != null ? error : {})));
  }
}
