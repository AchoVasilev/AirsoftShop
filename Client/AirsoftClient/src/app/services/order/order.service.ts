import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderDetailsModel } from 'src/app/models/orders/orderDetailsModel';
import { OrderInputModel } from 'src/app/models/orders/orderInputModel';
import { OrderListModel } from 'src/app/models/orders/orderListModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl: string = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  createOrder(data: OrderInputModel): Observable<string> {
    return this.httpClient.post<string>(`${this.apiUrl}/orders`, data);
  }

  getClientOrders(): Observable<OrderListModel[]> {
    return this.httpClient.get<OrderListModel[]>(`${this.apiUrl}/orders/client`);
  }

  getOrderDetails(orderId: string): Observable<OrderDetailsModel> {
    return this.httpClient.get<OrderDetailsModel>(`${this.apiUrl}/orders/${orderId}`, {
      params: { orderId }
    })
  }
}
