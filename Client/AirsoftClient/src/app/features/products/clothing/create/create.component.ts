import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SubCategoryViewModel } from 'src/app/models/categories/subCategoryViewModel';
import { CategoryService } from 'src/app/services/categories/category.service';
import { ClothingService } from 'src/app/services/products/clothing/clothing.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  isLoaded: boolean = false;
  isLoading: boolean = true;

  createClothingFormGroup: FormGroup = this.formBuilder.group({
    'name': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'manufacturer': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'color': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'images': new FormControl(null, [Validators.required]),
    'subCategoryId': new FormControl(null, [Validators.required]),
    'material': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'price': new FormControl(null, [Validators.required]),
    'size': new FormControl(null, [Validators.required, Validators.min(2), Validators.max(70)]),
    'description': new FormControl(null, [Validators.required, Validators.minLength(2), Validators.maxLength(1000)])
  });

  files: File[] = [];
  categories: SubCategoryViewModel[] | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private categoryService: CategoryService,
    private clothingService: ClothingService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadCategories();

    this.isLoaded = true;
    this.isLoading = false;
  }

  createClothingHandler() {
    this.isLoaded = false;
    this.isLoading = true;

    const {
      name,
      manufacturer,
      images,
      color,
      material,
      subCategoryId,
      size,
      price,
      description } = this.createClothingFormGroup.value;

    const body = new FormData();
    body.append('name', name);
    body.append('manufacturer', manufacturer);

    for (let index = 0; index < this.files.length; index++) {
      body.append('images', this.files[index]);
    }

    body.append('color', color);
    body.append('material', material);
    body.append('size', size);
    body.append('subCategoryId', subCategoryId);
    body.append('price', price);
    body.append('description', description);

    this.clothingService.createClothing(body)
      .subscribe({
        next: (res: any) => {
          this.toastr.success("Успешно добавихте нов артикул");
          this.isLoaded = true;
          this.isLoading = false;

          this.router.navigate([`/products/guns/${res.name}/${res.id}`])
        },
        complete: () => {
          this.isLoaded = true;
          this.isLoading = false;
        }
      });
  }

  loadCategories() {
    this.categoryService.loadClothingSubcategories()
      .subscribe(categories => this.categories = categories);
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.createClothingFormGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  onChange(event: any) {
    this.files = event.target.files;
  }

  notReady() {
    this.router.navigate(['/building']);
  }
}
