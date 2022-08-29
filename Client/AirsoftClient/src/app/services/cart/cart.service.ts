import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddToCartResultModel } from 'src/app/models/cart/addToCartResultModel';
import { CartDeliveryViewModel } from 'src/app/models/cart/cartDeliveryViewModel';
import { CartViewModel } from 'src/app/models/cart/cartViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  AddItem(gunId: string): Observable<AddToCartResultModel> {
    return this.httpClient.post<AddToCartResultModel>(`${this.apiUrl}/carts`, gunId);
  }

  GetItems(): Observable<CartViewModel[]> {
    return this.httpClient.get<CartViewModel[]>(`${this.apiUrl}/carts`);
  }

  RemoveItem(itemId: string): Observable<Object> {
    return this.httpClient.delete(`${this.apiUrl}/carts`, {
      params: { itemId }
    });
  }

  GetCartDeliveryData(): Observable<CartDeliveryViewModel> {
    return this.httpClient.get<CartDeliveryViewModel>(`${this.apiUrl}/carts/deliveryData`);
  }

  GetItemsCountAndPrice(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiUrl}/carts/getNavData`);
  }
}
