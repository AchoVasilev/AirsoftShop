import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/categories/category.service';
import { ProductService } from 'src/app/services/products/product.service';
import { GunSubCategoryViewModel } from 'src/app/models/categories/gunSubCategoryViewModel';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  isAddingGun: boolean = false;
  isAddingTacticalEquipment: boolean = false;
  isAddingMaintanence: boolean = false;
  isAddingAddOns: boolean = false;
  isAddingAccessories: boolean = false;
  isAddingCloting: boolean = false;

  isLoaded: boolean = false;
  isLoading: boolean = true;

  file!: File;
  gunSubCategories: GunSubCategoryViewModel[] = [];

  gunCreateFormGroup: FormGroup = this.formBuilder.group({
    'name': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'manufacturer': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'image': new FormControl(null, [Validators.required]),
    'power': new FormControl(null, [Validators.required, Validators.max(999), Validators.min(1)]),
    'color': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'weight': new FormControl(null, [Validators.required, Validators.max(9999), Validators.min(1)]),
    'magazine': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'capacity': new FormControl(null, [Validators.required, Validators.max(999), Validators.min(1)]),
    'speed': new FormControl(null, [Validators.required, Validators.max(999), Validators.min(1)]),
    'firing': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'length': new FormControl(null, [Validators.required, Validators.min(1), Validators.max(9999)]),
    'barrel': new FormControl(null, [Validators.required, Validators.min(1), Validators.max(9999)]),
    'propulsion': new FormControl(null, [Validators.required]),
    'material': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'blowback': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'hopup': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'subCategoryName': new FormControl(null, [Validators.required]),
    'price': new FormControl(null, [Validators.required])
  });

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getGunSubCategories();
    this.isLoaded = true;
    this.isLoading = false;
  }

  getGunSubCategories() {
    this.categoryService.loadGunSubcategories()
      .subscribe(subCat => this.gunSubCategories = subCat);
  }

  onChange(event: any) {
    this.file = <File>event.target.files[0];
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.gunCreateFormGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  notReady() {
    this.router.navigate(['/building']);
  }

  createGunHandler() {
    this.isLoaded = false;
    this.isLoading = true;

    const {
      name,
      manufacturer,
      image,
      power,
      color,
      weight,
      magazine,
      capacity,
      speed,
      firing,
      length,
      barrel,
      propulsion,
      material,
      blowback,
      hopup,
      subCategoryName,
      price } = this.gunCreateFormGroup.value;

    const body = new FormData();
    body.append('name', name);
    body.append('manufacturer', manufacturer);
    body.append('image', this.file);
    body.append('power', power);
    body.append('color', color);
    body.append('weight', weight);
    body.append('magazine', magazine);
    body.append('capacity', capacity);
    body.append('speed', speed);
    body.append('firing', firing);
    body.append('length', length);
    body.append('barrel', barrel);
    body.append('propulsion', propulsion);
    body.append('material', material);
    body.append('blowback', blowback);
    body.append('hopup', hopup);
    body.append('subCategoryName', subCategoryName);
    body.append('price', price);

    this.productService.createGun(body)
      .subscribe({
        next: (res: any) => {
          this.toastr.success("Успешно добавихте нов артикул");
          this.isLoaded = true;
          this.isLoading = false;

          this.router.navigate([`/products/${res.name}/${res.gunId}`])
        }
      })
  }
}
