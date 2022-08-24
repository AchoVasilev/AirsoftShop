import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DealerIdObj } from 'src/app/models/dealers/dealerIdObj';
import { GunDetailsViewModel } from 'src/app/models/products/guns/gunDetailsViewModel';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { DealerService } from 'src/app/services/dealer/dealer.service';
import { ProductService } from 'src/app/services/products/product.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  isLoading: boolean = true;
  isLoaded: boolean = false;
  isLoggedIn: boolean = true;
  isClient: boolean = true;
  isOwner: boolean = false;
  dealerObj: DealerIdObj | undefined;
  gun!: GunDetailsViewModel;

  private itemsCount: number = 0;
  private cartItemsCount: number = 0;
  private price: number = 0;
  private gunId = this.route.snapshot.params['id'];

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private authService: AuthService,
    private cartService: CartService,
    private dealerService: DealerService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getGunDetails();
    this.getDealerId();
    this.isLoggedIn = this.authService.isAuthenticated();
    this.isClient = this.authService.getClient();

    this.isOwner = this.isLoggedIn && this.dealerObj?.id == this.gun?.dealerId
    this.isLoading = false;
    this.isLoaded = true;
  }

  getGunDetails() {
    this.productService.getGunDetails(this.gunId)
      .subscribe(res => this.gun = res);
  }

  getDealerId(): void {
    this.dealerService.getDealerId()
      .subscribe(res => {
        this.dealerObj = res;
      });
  }

  addToBasket(gunId: number, price: number) {
    this.isLoading = true;
    this.isLoaded = false;

    this.cartService.AddItem(gunId)
      .subscribe({
        next: (result) => {
          this.toastr.success("Успешно добавяне!");
          this.itemsCount = this.cartItemsCount + 1;
          this.cartItemsCount = this.itemsCount;

          // this.price = +price;
          // this.price = (+this.cartItemsPrice) + (+this.price);
          // this.cartItemsPrice = this.price;
        },
        complete: () => {
          this.isLoading = false;
          this.isLoaded = true;
        }
      })
  }

  onDelete(gunId: string) {
    this.isLoading = true;
    this.isLoaded = false;
    this.productService.deleteGun(gunId)
      .subscribe({
        next: () => {
          this.toastr.success("Успешно изтриване");
          this.isLoading = false;
          this.isLoaded = true;

          this.router.navigate(['/products/mine']);
        }
      })
  }
}