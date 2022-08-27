import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderInputModel } from 'src/app/models/orders/orderInputModel';
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
}
