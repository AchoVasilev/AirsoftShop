import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SubCategoryViewModel } from 'src/app/models/categories/subCategoryViewModel';
import { GunDetailsViewModel } from 'src/app/models/products/guns/gunDetailsViewModel';
import { GunEditModel } from 'src/app/models/products/guns/gunEditModel';
import { CategoryService } from 'src/app/services/categories/category.service';
import { GunService } from 'src/app/services/products/guns/gun.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  isLoaded: boolean = false;
  isLoading: boolean = true;
  gun!: GunDetailsViewModel;
  gunId: string = this.route.snapshot.params['id'];
  file!: File;
  gunSubCategories: SubCategoryViewModel[] = [];
  gunCreateFormGroup: FormGroup = this.formBuilder.group({
    'name': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'manufacturer': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
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
    'subCategoryId': new FormControl(null, [Validators.required]),
    'price': new FormControl(null, [Validators.required])
  });

  constructor(
    private categoryService: CategoryService,
    private formBuilder: FormBuilder,
    private gunService: GunService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getGun();
    this.getGunSubCategories();

    this.gunCreateFormGroup.patchValue({
      name: this.gun.name,
      manufacturer: this.gun.manufacturer,
      power: this.gun.power,
      color: this.gun.color,
      weight: this.gun.weight,
      magazine: this.gun.magazine,
      capacity: this.gun.capacity,
      speed: this.gun.speed,
      firing: this.gun.firing,
      length: this.gun.length,
      barrel: this.gun.barrel,
      propulsion: this.gun.propulsion,
      material: this.gun.material,
      blowback: this.gun.blowback,
      hopup: this.gun.hopup,
      price: this.gun.price,
    });

    this.isLoaded = true;
    this.isLoading = false;
  }

  getGun() {
    this.gunService.getGunDetails(this.gunId)
      .subscribe(gun => this.gun = gun);
  }

  getGunSubCategories() {
    this.categoryService.loadGunSubcategories()
      .subscribe(subCat => this.gunSubCategories = subCat);
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.gunCreateFormGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  notReady() {
    this.router.navigate(['/building']);
  }

  editGun() {
    this.isLoaded = false;
    this.isLoading = true;

    const {
      name,
      manufacturer,
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
      subCategoryId,
      hopup,
      price,
    } = this.gunCreateFormGroup.value;

    const editModel: GunEditModel = {
      name,
      manufacturer,
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
      subCategoryId,
      hopup,
      price
    };

    this.gunService.editGun(editModel)
      .subscribe({
        next: () => {
          this.toastr.success("Успешна промяна");

          this.isLoaded = true;
          this.isLoading = false;

          this.gunCreateFormGroup.reset();
          this.router.navigate(['/products/mine'])
        }
      })
  }
}
