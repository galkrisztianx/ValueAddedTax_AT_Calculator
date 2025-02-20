import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ValueAddedTaxCalculatorService } from "./services/vat-calculator.service";
import { ValueAddedTaxCalculatorComponent } from "./components/vat-calculator.component";
import { MatRadioModule } from '@angular/material/radio';
import { FormsModule } from "@angular/forms";
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { CalculatorInputDirective } from "./directives/calculator-input-validator";
import { PositiveNumberValidatorDirective } from "./directives/positive-number-validator.directive";


@NgModule({
    imports: [
      CommonModule,
      MatRadioModule,
      FormsModule, 
      MatFormFieldModule, 
      MatInputModule,
      MatButtonModule,
      MatIconModule,
      MatGridListModule,
      CalculatorInputDirective,
      PositiveNumberValidatorDirective
    ],
    providers: [ValueAddedTaxCalculatorService],
    declarations: [ValueAddedTaxCalculatorComponent],
    exports: [ValueAddedTaxCalculatorComponent],
  })
  export class ValueAddedTaxCalculatorModule {}