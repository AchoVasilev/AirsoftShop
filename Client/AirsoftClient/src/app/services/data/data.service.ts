import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CartViewModel } from 'src/app/models/cart/cartViewModel';
import { UserClientViewModel } from 'src/app/models/client/userClientViewModel';
import { GunDetailsViewModel } from 'src/app/models/products/guns/gunDetailsViewModel';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private cartItemsCountSource = new BehaviorSubject<number>(0);
  cartItemsCount: Observable<number> = this.cartItemsCountSource.asObservable();
  changeCartItemsCount(value: number) {
    this.cartItemsCountSource.next(value);
  }

  private cartItemsPriceSource = new BehaviorSubject<number>(0);
  cartItemsPrice: Observable<number> = this.cartItemsPriceSource.asObservable();
  changeCartItemsPrice(value: number) {
    this.cartItemsPriceSource.next(value);
  }

  private finalPriceSource = new BehaviorSubject<number>(0);
  finalPrice: Observable<number> = this.finalPriceSource.asObservable();
  changeFinalPrice(value: number) {
    this.finalPriceSource.next(value);
  }

  private initialObjectSource: any = null;

  private userDataSource = new BehaviorSubject<UserClientViewModel>(this.initialObjectSource);
  userData: Observable<UserClientViewModel> = this.userDataSource.asObservable();
  changeUserData(value: UserClientViewModel) {
    this.userDataSource.next(value);
  }

  private cartItemsSource = new BehaviorSubject<CartViewModel[]>(this.initialObjectSource);
  cartItems: Observable<CartViewModel[]> = this.cartItemsSource.asObservable();
  changeCartItems(value: CartViewModel[]) {
    this.cartItemsSource.next(value);
  }

  private gunSource = new BehaviorSubject<GunDetailsViewModel>(this.initialObjectSource);
  gun: Observable<GunDetailsViewModel> = this.gunSource.asObservable();
  changeGun(value: GunDetailsViewModel) {
    this.gunSource.next(value);
  }

  private courierIdSource = new BehaviorSubject<number>(0);
  courierId: Observable<number> = this.courierIdSource.asObservable();
  changeCourierId(value: number) {
    this.courierIdSource.next(value);
  }

  private courierNameSource = new BehaviorSubject<string>('');
  courierName: Observable<string> = this.courierNameSource.asObservable();
  changeCourierName(value: string) {
    this.courierNameSource.next(value);
  }

  private courierDeliveryPriceSource = new BehaviorSubject<number>(0);
  courierDeliveryPrice: Observable<number> = this.courierDeliveryPriceSource.asObservable();
  changeCourierDeliveryPrice(value: number) {
    this.courierDeliveryPriceSource.next(value);
  }

  private paymentTypeSource = new BehaviorSubject<string>('');
  paymentType: Observable<string> = this.paymentTypeSource.asObservable();
  changePaymentType(value: string) {
    this.paymentTypeSource.next(value);
  }

  constructor() { }
}
