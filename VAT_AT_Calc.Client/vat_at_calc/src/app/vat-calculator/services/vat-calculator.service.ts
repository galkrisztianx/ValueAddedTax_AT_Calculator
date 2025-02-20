import { Injectable } from '@angular/core';
import { delay, Observable, of } from 'rxjs';
import { ValueAddedTaxAmountsInterface } from '../types/vat-amounts.interfaces';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ValueAddedTaxCalculatorService {

  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  calculateValueAddedTaxAmounts(valueAddedTaxAmounts: ValueAddedTaxAmountsInterface): Observable<ValueAddedTaxAmountsInterface> {
    return this.http.post<ValueAddedTaxAmountsInterface>(`${this.apiUrl}/Calculator/CalculateValueAddedTax`, valueAddedTaxAmounts);
  }
}