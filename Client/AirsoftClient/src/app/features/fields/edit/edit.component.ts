import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CityModel } from 'src/app/models/address/CityModel';
import { FieldDetailsModel } from 'src/app/models/fields/fieldDetailsModel';
import { FieldEditModel } from 'src/app/models/fields/fieldEditModel';
import { CityService } from 'src/app/services/city/city.service';
import { DataService } from 'src/app/services/data/data.service';
import { FieldService } from 'src/app/services/fields/field.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  cities: CityModel[] = [];
  isLoaded: boolean = false;
  isLoading: boolean = true;
  zipCode: number = 0;
  field!: FieldDetailsModel;

  formGroup: FormGroup = this.formBuilder.group({
    'cityId': new FormControl('', [Validators.required]),
    'zipCode': new FormControl({ value: '', disabled: true }, [Validators.required]),
    'streetName': new FormControl('', [Validators.required, Validators.maxLength(40), Validators.minLength(2)]),
    'description': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(1000)])
  });

  constructor(
    private formBuilder: FormBuilder,
    private cityService: CityService,
    private fieldService: FieldService,
    private dataService: DataService,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.getCities();
    this.dataService.fieldDetailsModel
      .subscribe(field => this.field = field);

    this.formGroup.patchValue({
      cityId: this.field.address.city.id,
      zipCode: this.field.address.city.zipCode,
      streetName: this.field.address.streetName,
      description: this.field.description
    });

    this.isLoaded = true;
    this.isLoading = false;
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.formGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  getCities() {
    this.cityService.loadCities()
      .subscribe(cities => this.cities = cities);
  }

  onOptionClick(zipCode: number) {
    this.zipCode = zipCode;
  }

  editField() {
    this.isLoaded = false;
    this.isLoading = true;

    let {
      cityId,
      streetName,
      description,
      zipCode
    } = this.formGroup.value;

    let model: FieldEditModel = {
      cityId,
      streetName,
      description,
      zipCode,
      id: this.field.id,
      dealerId: this.field.dealerId
    };

    this.fieldService.edit(model)
      .subscribe({
        next: () => {
          this.toastr.success("Успешна промяна");

          this.isLoaded = true;
          this.isLoading = false;

          this.formGroup.reset();
          this.router.navigate(['/fields/' + model.id])
        }
      })
  }
}
