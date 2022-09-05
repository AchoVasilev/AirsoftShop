import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { WishListModel } from 'src/app/models/wishList/WishListModel';
import { CartService } from 'src/app/services/cart/cart.service';
import { DataService } from 'src/app/services/data/data.service';
import { WishListService } from 'src/app/services/wishList/wish-list.service';

@Component({
  selector: 'app-wish-list',
  templateUrl: './wish-list.component.html',
  styleUrls: ['./wish-list.component.css']
})
export class WishListComponent implements OnInit {
  isLoading: boolean = true;
  isLoaded: boolean = false;

  items: WishListModel[] = [];
  currentCartItemsCount: number = 0;
  currentCartTotalPrice: number = 0;

  constructor(
    private wishListService: WishListService,
    private cartService: CartService,
    private dataService: DataService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getItemsFromWishList();
    this.dataService.cartItemsCount.subscribe(count => this.currentCartItemsCount = count);
    this.dataService.cartItemsPrice.subscribe(price => this.currentCartTotalPrice = price);

    this.isLoading = false;
    this.isLoaded = true;
  }

  getItemsFromWishList() {
    this.wishListService.getItems()
      .subscribe(items => this.items = items);
  }

  addToCart(id: string, price: number) {
    this.isLoading = true;
    this.isLoaded = false;

    this.cartService.AddItem(id)
      .subscribe({
        next: (result) => {
          this.toastr.success("Успешно добавяне!");
          this.currentCartItemsCount = result.itemsCount;

          this.dataService.changeCartItemsCount(this.currentCartItemsCount);

          this.currentCartTotalPrice = (+this.currentCartTotalPrice) + (+price);

          this.dataService.changeCartItemsPrice(this.currentCartTotalPrice);

          this.onRemove(id);
        },
        complete: () => {
          this.isLoading = false;
          this.isLoaded = true;
        }
      });
  }

  addAllToCart(items: WishListModel[]) {
    this.isLoading = true;
    this.isLoaded = false;

    const itemIds = items.map(function (item) {
      return item.id;
    });

    this.cartService.AddItems(itemIds)
      .subscribe({
        next: (result) => {
          this.toastr.success("Успешно добавяне!");
          this.currentCartItemsCount = result.itemsCount;

          this.dataService.changeCartItemsCount(this.currentCartItemsCount);

          for (let i = 0; i < items.length; i++) {
            this.currentCartTotalPrice = this.currentCartTotalPrice + items[i].price;
          }

          this.dataService.changeCartItemsPrice(this.currentCartTotalPrice);
          this.removeItems(itemIds);
        },
        complete: () => {
          this.isLoading = false;
          this.isLoaded = true;
        }
      });
  }

  onRemove(id: string) {
    this.isLoading = true;
    this.isLoaded = false;

    this.wishListService.removeItem(id)
      .subscribe({
        next: () => {
          this.toastr.success("Успешно премахване!");
        },
        complete: () => {
          this.isLoading = false;
          this.isLoaded = true;
        }
      });
  }

  private removeItems(ids: string[]) {
    this.isLoading = true;
    this.isLoaded = false;

    this.wishListService.removeItems(ids)
      .subscribe({
        complete: () => {
          this.isLoading = false;
          this.isLoaded = true;
        }
      });
  }
}
