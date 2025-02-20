import { Component, QueryList, signal, ViewChild, ViewChildren } from '@angular/core';
import { MatRadioChange, MatRadioGroup } from '@angular/material/radio';
import { ValueAddedTaxAmountsInterface } from '../types/vat-amounts.interfaces';
import { ValueAddedTaxCalculatorService } from '../services/vat-calculator.service';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs';

@Component({
  selector: 'app-vat-calculator',
  templateUrl: './vat-calculator.component.html',
  styleUrl: './vat-calculator.component.css'
})
export class ValueAddedTaxCalculatorComponent {

  @ViewChild(NgForm) calculatorForm: NgForm | undefined;
  @ViewChildren(MatRadioGroup) vatRateRadioControl: QueryList<MatRadioGroup> | undefined;

  protected readonly isSubmitted = signal<Boolean>(false); // for some reason in this angular version the form's own submitted value was bugged


  protected readonly valueAddedTaxAmounts = signal<ValueAddedTaxAmountsInterface>({
    net: null,
    gross: null,
    valueAddedTaxAmount: null,
    valueAddedTaxRate: null
  });
  

  constructor(private valueAddedTaxCalculatorService: ValueAddedTaxCalculatorService) {
  }

  onCalcRadioButtonChange(event: MatRadioChange) {
    const selectedValueAddedTaxRate = event.value as number;
    this.valueAddedTaxAmounts.update(oldValue => ({ ...oldValue, valueAddedTaxRate : selectedValueAddedTaxRate}));
  }

  onSubmitCalculatorForm() {
    this.isSubmitted.set(true);
    if (this.validateCalculatorForm()) {
      this.valueAddedTaxCalculatorService.calculateValueAddedTaxAmounts(this.valueAddedTaxAmounts()).pipe(first()).subscribe(
        (calculatedValues) => {
          this.isSubmitted.set(false);
          this.valueAddedTaxAmounts.set(calculatedValues)
        }
      );
    }
  }

  validateCalculatorForm(): boolean {
    if(!this.calculatorForm) return false;
    if(this.calculatorForm.form.hasError("wrongInputs")) return false;

    return true;
  }

  resetCalculatorForm() {
    this.valueAddedTaxAmounts.set({
      net: null,
      gross: null,
      valueAddedTaxAmount: null,
      valueAddedTaxRate: null
    });

    if(!this.vatRateRadioControl) return;

    this.vatRateRadioControl.forEach((radio) => (radio.value = 'None'));

    if(!this.calculatorForm) return;
    this.calculatorForm.resetForm();
    this.calculatorForm.form.markAsPristine();
    this.calculatorForm.form.markAsUntouched();
    this.calculatorForm.form.updateValueAndValidity();
    this.isSubmitted.set(false);
  }
}