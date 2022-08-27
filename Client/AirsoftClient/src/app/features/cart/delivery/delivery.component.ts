import { Component, ElementRef, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { Router } from '@angular/router';
import { CartDeliveryViewModel } from 'src/app/models/cart/cartDeliveryViewModel';
import { CartService } from 'src/app/services/cart/cart.service';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent implements OnInit {
  deliveryData: CartDeliveryViewModel | undefined;
  isLoaded: boolean = false;
  isLoading: boolean = true;
  deliveryPrice: number = 0;
  finalPrice: number = 0;

  @ViewChildren('firms')
  couriers!: QueryList<any>;

  @ViewChild('cash')
  cash!: ElementRef;

  @ViewChild('card')
  card!: ElementRef;

  itemsPrice: number = 0;
  courierPrice: number = 0;
  totalPrice: number = 0;
  courierId: number = 0;
  paymentType: string = '';
  courierName: string = '';

  setPaymentType(value: string) {
    this.dataService.changePaymentType(value);
  }

  setFinalPrice(value: number) {
    this.dataService.changeFinalPrice(value);
  }

  setCourierPrice(value: number) {
    this.dataService.changeCourierDeliveryPrice(value);
  }

  setCourierId(value: number) {
    this.dataService.changeCourierId(value);
  }

  setCourierName(value: string) {
    this.dataService.changeCourierName(value);
  }

  constructor(private cartService: CartService, private dataService: DataService, private router: Router) { }

  ngOnInit(): void {
    this.getDeliveryData();
    this.isLoaded = true;
    this.isLoading = false;

    this.dataService.courierDeliveryPrice.subscribe(price => this.courierPrice = price);
    this.dataService.cartItemsPrice.subscribe(price => this.itemsPrice = price);
    this.dataService.finalPrice.subscribe(price => this.totalPrice = price);
    this.dataService.courierId.subscribe(id => this.courierId = id);
    this.dataService.paymentType.subscribe(type => this.paymentType = type);
    this.dataService.courierName.subscribe(name => this.courierName = name);
  }

  getDeliveryData(): void {
    this.cartService.GetCartDeliveryData()
      .subscribe(data => this.deliveryData = data);
  }

  radioBtnClick(event: any) {
    if (event.target.id == this.card.nativeElement.id) {
      this.card.nativeElement.checked = true;
      this.cash.nativeElement.checked = false;
    } else if (event.target.id == this.cash.nativeElement.id) {
      this.cash.nativeElement.checked = true;
      this.card.nativeElement.checked = false;
    }

    this.paymentType = event.target.id;
    this.setPaymentType(this.paymentType);
  }

  courierBtnClick(event: any, courierId: number, courierName: string) {
    this.couriers.toArray().forEach(courier => {
      courier.nativeElement.classList.remove('selected');
    });

    this.finalPrice = this.itemsPrice;

    event.currentTarget.classList.add('selected');

    for (const courier of this.deliveryData!.couriers) {
      if (event.currentTarget.id == courier.name && this.itemsPrice < 500) {
        this.deliveryPrice = courier.deliveryPrice;
        this.setCourierPrice(this.deliveryPrice);
      }
    }

    this.courierId = courierId;
    this.setCourierId(this.courierId);

    this.courierName = courierName;
    this.setCourierName(this.courierName);

    this.courierPrice = this.deliveryPrice;
    this.setCourierPrice(this.courierPrice);

    this.finalPrice = (+this.finalPrice) + (+this.deliveryPrice);

    this.totalPrice = this.finalPrice;
    this.setFinalPrice(this.totalPrice);
  }

  summaryBtnClick() {
    this.router.navigate(['/cart/summary']);
  }
}
