import { Directive, Input } from '@angular/core';
import { NG_VALIDATORS, Validator, ValidationErrors, AbstractControl, FormControl } from '@angular/forms';

@Directive({
  selector: '[appCalculatorInputValidator]',
  providers: [{ provide: NG_VALIDATORS, useExisting: CalculatorInputDirective, multi: true }],
  standalone: true
})
export class CalculatorInputDirective implements Validator {
  @Input('appCalculatorInputValidator') inputs: string[] = [];

  constructor() { }

  validate(form: AbstractControl): ValidationErrors | null {
    const formControl = form as FormControl;
    const netControl = formControl.get(this.inputs[0]);
    const vatAmountControl = formControl.get(this.inputs[1]);
    const grossControl = formControl.get(this.inputs[2]);

    if (!netControl || !vatAmountControl || !grossControl) {
        return null;
    }

    let inputCounter = 0;
    if (netControl.value != null) inputCounter += 1;
    if (vatAmountControl.value != null) inputCounter += 1;
    if (grossControl.value != null) inputCounter += 1;

    if(inputCounter != 1) {
        formControl.setErrors({ wrongInputs: true });
        return { wrongInputs: true };
    } else {
        return null;
    }
  }
}