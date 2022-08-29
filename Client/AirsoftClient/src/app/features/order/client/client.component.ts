import { Component, OnInit } from '@angular/core';
import { OrderGunViewModel } from 'src/app/models/orders/orderGunViewModel';
import { OrderListModel } from 'src/app/models/orders/orderListModel';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit {
  orderList: OrderListModel[] = [];
  isLoading: boolean = true;
  isLoaded: boolean = false;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadClientOrders();
    this.isLoaded = true;
    this.isLoading = false;
  }

  loadClientOrders(): void {
    this.orderService.getClientOrders()
      .subscribe(res => this.orderList = res);
  }
}
