import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartViewModel } from 'src/app/models/cart/cartViewModel';
import { UserClientViewModel } from 'src/app/models/client/userClientViewModel';
import { OrderInputModel } from 'src/app/models/orders/orderInputModel';
import { DataService } from 'src/app/services/data/data.service';
import { OrderService } from 'src/app/services/order/order.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css']
})
export class SummaryComponent implements OnInit {
  courierId: number = 0;
  deliveryPrice: number = 0;
  finalPrice: number = 0;
  cartItemsCount: number = 0;
  cartItemsPrice: number = 0;

  courierName: string = '';
  paymentType: string = '';

  orderData!: OrderInputModel;
  userData!: UserClientViewModel;
  cartItems: CartViewModel[] = [];

  setCartItemsCount(value: number) {
    this.dataService.changeCartItemsCount(value);
  }

  setCartItemsPrice(value: number) {
    this.dataService.changeCartItemsCount(value);
  }

  constructor(private dataService: DataService, private orderService: OrderService, private router: Router) { }

  ngOnInit(): void {
    this.dataService.courierId.subscribe(id => this.courierId = id);
    this.dataService.courierDeliveryPrice.subscribe(price => this.deliveryPrice = price);
    this.dataService.finalPrice.subscribe(price => this.finalPrice = price);
    this.dataService.cartItemsCount.subscribe(count => this.cartItemsCount = count);
    this.dataService.cartItemsPrice.subscribe(price => this.cartItemsPrice = price);
    this.dataService.courierName.subscribe(name => this.courierName = name);
    this.dataService.paymentType.subscribe(type => this.paymentType = type);
    this.dataService.userData.subscribe(data => this.userData = data);
    this.dataService.cartItems.subscribe(items => this.cartItems = items);
  }

  sendOrder() {
    const gunsIds: string[] = [];
    this.cartItems.forEach(el => {
      gunsIds.push(el.id);
    });

    this.orderData = {
      totalPrice: this.finalPrice,
      paymentType: this.paymentType,
      courierId: this.courierId,
      gunsIds
    };

    this.orderService.createOrder(this.orderData)
      .subscribe({
        next: (res: any) => {
          this.cartItemsCount = 0;
          this.cartItemsPrice = 0;
          this.router.navigate(['/orders/client']);
        }
      })
  }
}
