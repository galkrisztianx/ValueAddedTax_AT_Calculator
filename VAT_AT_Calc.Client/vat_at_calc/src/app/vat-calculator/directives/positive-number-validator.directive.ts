import { Directive } from '@angular/core';
import {
  Validator,
  NG_VALIDATORS,
  AbstractControl,
} from '@angular/forms';

@Directive({
  selector: '[appPositiveNumber]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: PositiveNumberValidatorDirective,
      multi: true,
    },
  ],
  standalone: true,
})
export class PositiveNumberValidatorDirective implements Validator {
  validate(control: AbstractControl): { [key: string]: any } | null {
    const value = control.value;
    return value != null && value <= 0 ? { positiveNumberError: true } : null;
  }
}
