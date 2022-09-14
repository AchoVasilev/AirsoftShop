import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, map, switchMap } from 'rxjs';
import { GunSubCategoryViewModel } from 'src/app/models/categories/gunSubCategoryViewModel';
import { GunsViewModel } from 'src/app/models/products/guns/gunsViewModel';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CartService } from 'src/app/services/cart/cart.service';
import { CategoryService } from 'src/app/services/categories/category.service';
import { DataService } from 'src/app/services/data/data.service';
import { GunService } from 'src/app/services/products/guns/gun.service';
import { WishListService } from 'src/app/services/wishList/wish-list.service';

@Component({
  selector: 'app-gun-list',
  templateUrl: './gun-list.component.html',
  styleUrls: ['./gun-list.component.css']
})
export class GunListComponent implements OnInit {
  subCategories: GunSubCategoryViewModel[] = [];
  isLoading: boolean = true;
  isLoaded: boolean = false;
  categoryName: string = '';

  canAdd: boolean = false;

  dealerFormGroup!: FormGroup;
  manufacturersFormGroup!: FormGroup;
  colorsFormGroup!: FormGroup;
  powersFormGroup!: FormGroup;
  sortingFormGroup!: FormGroup;

  page: number = 1;
  itemsPerPage: number = 9;
  private orderBy: string = 'alphabetical';
  private dealers: string[] = [];
  private manufacturers: string[] = [];
  private colors: string[] = [];
  private powers: number[] = [];
  private price: number = 0;
  private itemsCount: number = 0;
  private cartItemsPrice: number = 0;
  private cartItemsCount: number = 0;

  private pageChanges = new BehaviorSubject(undefined);
  allGuns!: GunsViewModel;

  @ViewChild('orderBy')
  orderByElement!: ElementRef;

  @ViewChild('count')
  countElement!: ElementRef;

  constructor(
    private gunService: GunService,
    private categoryService: CategoryService,
    private cartService: CartService,
    private authService: AuthService,
    private dataService: DataService,
    private wishListService: WishListService,
    private toastr: ToastrService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.canAdd = this.authService.isAuthenticated() && this.authService.getClient();
    this.getAllGuns();
    this.getGunSubCategories();

    this.dataService.cartItemsPrice.subscribe(price => this.cartItemsPrice = price);
    this.dataService.cartItemsCount.subscribe(count => this.cartItemsCount = count);

    this.dealerFormGroup = this.formBuilder.group({
      dealers: this.formBuilder.array([])
    });

    this.manufacturersFormGroup = this.formBuilder.group({
      manufacturers: this.formBuilder.array([])
    });

    this.colorsFormGroup = this.formBuilder.group({
      colors: this.formBuilder.array([])
    });

    this.powersFormGroup = this.formBuilder.group({
      powers: this.formBuilder.array([])
    });

    this.sortingFormGroup = this.formBuilder.group({
      'orderBy': new FormControl('alphabetical')
    });
  }

  getAllGuns() {
    this.pageChanges;
    this.route.params.pipe(
      map(params => params['name'] ? this.categoryName = params['name'] : this.categoryName = ''),
      switchMap(() =>
        this.gunService
          .getAllGunsQuery(this.categoryName, this.itemsPerPage, this.orderBy, this.dealers, this.manufacturers, this.colors, this.powers, this.page))
    ).subscribe(res => {
      this.allGuns = res;
      this.isLoaded = true;
      this.isLoading = false;
    });
  }

  getGunSubCategories(): void {
    this.categoryService.loadGunSubcategories()
      .subscribe(subs => this.subCategories = subs);
  }

  sortingCheck() {
    const count = this.countElement.nativeElement.value;
    const orderBy = this.orderByElement.nativeElement.value;
    this.itemsPerPage = count;
    this.orderBy = orderBy;

    this.getAllGuns();
  }

  onChange(itemName: any, formGroup: FormGroup, groupName: string, isChecked: any) {
    const itemsArr = (formGroup.controls[groupName] as FormArray);

    if (isChecked.checked) {
      itemsArr.push(new FormControl(itemName));
    } else {
      const index = itemsArr.controls.findIndex(x => x.value === itemName);
      itemsArr.removeAt(index);
    }
  }

  filterByDealers() {
    const data: string[] = this.dealerFormGroup.value['dealers'];
    this.dealers = data;

    this.getAllGuns();
  }

  filterByManufacturers() {
    const data: string[] = this.manufacturersFormGroup.value['manufacturers'];
    this.manufacturers = data;

    this.getAllGuns();
  }

  filterByColors() {
    const data: string[] = this.colorsFormGroup.value['colors'];
    this.colors = data;

    this.getAllGuns();
  }

  filterByPowers() {
    const data: number[] = this.powersFormGroup.value['powers'];
    this.powers = data;

    this.getAllGuns();
  }

  addToBasket(gunId: string, price: number) {
    this.isLoaded = false;
    this.isLoading = true;
    this.cartService.AddItem(gunId)
      .subscribe({
        next: (result) => {
          this.toastr.success("Успешно добавяне!");
          this.itemsCount = this.cartItemsCount + 1;
          this.dataService.changeCartItemsCount(this.itemsCount);

          this.price = +price;
          this.price = (+this.cartItemsPrice) + (+this.price);
          this.dataService.changeCartItemsPrice(this.price);
        },
        complete: () => {
          this.isLoaded = true;
          this.isLoading = false;
        }
      })
  }

  addToWishList(id: string) {
    this.wishListService.addItem(id)
      .subscribe({
        next: () => {
          this.toastr.success("Успешно добавяне!");
        }
      });
  }

  goOnePageBack() {
    this.page--;
    this.getAllGuns();
  }

  goOnePageForward() {
    this.page++;
    this.getAllGuns();
  }
}
