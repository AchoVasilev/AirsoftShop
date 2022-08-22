import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddToCartResultModel } from 'src/app/models/cart/addToCartResultModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  AddItem(gunId: number): Observable<AddToCartResultModel> {
    return this.httpClient.post<AddToCartResultModel>(`${this.apiUrl}/carts`, gunId);
  }
}
