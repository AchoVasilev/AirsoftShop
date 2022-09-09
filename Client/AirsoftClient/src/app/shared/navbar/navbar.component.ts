import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { shareReplay } from 'rxjs';
import { CategoryViewModel } from 'src/app/models/categories/categoryViewModel';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { CategoryService } from 'src/app/services/categories/category.service';
import { DataService } from 'src/app/services/data/data.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  categories: CategoryViewModel[] = [];

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated();
  }

  get isClient(): boolean {
    return this.authService.getClient();
  }

  isDealer: boolean = false;

  cartItemsPrice: number = 0;
  cartItemsCount: number = 0;

  constructor(
    private authService: AuthService,
    private categoryService: CategoryService,
    private dataService: DataService,
    private cartService: CartService,
    private router: Router) { }

  ngOnInit(): void {
    if (this.isLoggedIn && this.isClient) {
      this.getCartData();
    }

    this.loadCategories();
    this.dataService.cartItemsCount.subscribe(count => this.cartItemsCount = count);
    this.dataService.cartItemsPrice.subscribe(price => this.cartItemsPrice = price);

    this.isDealer = !this.isClient && this.isLoggedIn;
  }

  getCartData(): void {
    this.cartService.GetItemsCountAndPrice()
      .subscribe(res => {
        this.dataService.changeCartItemsCount(res.itemsCount);
        this.dataService.changeCartItemsPrice(res.totalPrice);
      })
  }

  loadCategories(): void {
    this.categoryService.loadCategories()
      .pipe(shareReplay(2))
      .subscribe(res => this.categories = res);
  }

  catalogClick(): void {
    let catalog = document.getElementById('nav-catalog')!;
    const icon = document.querySelector('#catalog-btn > i')!;

    if (catalog.style.display == '' || catalog.style.display == 'none') {
      catalog.style.display = 'block';
    } else {
      catalog.style.display = 'none';
    }

    if (icon.classList.contains('fa-bars')) {
      icon.classList.remove('fa-bars');
      icon.classList.add('fa-xmark');
    } else if (icon.classList.contains('fa-xmark')) {
      icon.classList.remove('fa-xmark');
      icon.classList.add('fa-bars');
    }
  }

  mouseOut(): void {
    let catalog = document.getElementById('nav-catalog')!;
    const icon = document.querySelector('#catalog-btn > i')!;
    catalog.style.display = 'none';

    if (icon.classList.contains('fa-xmark')) {
      icon.classList.remove('fa-xmark');
      icon.classList.add('fa-bars');
    }
  }

  logOut(): void {
    this.authService.logOut();
    this.router.navigate(['/']);
  }
}
