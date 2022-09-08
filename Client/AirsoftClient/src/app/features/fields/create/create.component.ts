import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CityViewModel } from 'src/app/models/address/cityViewModel';
import { CityService } from 'src/app/services/city/city.service';
import { FieldService } from 'src/app/services/fields/field.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  private files: File[] = [];

  cities: CityViewModel[] = [];

  isLoaded: boolean = false;
  isLoading: boolean = true;

  createFieldFormGroup: FormGroup = this.formBuilder.group({
    'cityId': new FormControl('', [Validators.required]),
    'zipCode': new FormControl('', [Validators.required]),
    'streetName': new FormControl('', [Validators.required, Validators.maxLength(40), Validators.minLength(2)]),
    'images': new FormControl('', Validators.required)
  });

  constructor(
    private cityService: CityService,
    private fieldService: FieldService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.getCities();

    this.isLoaded = true;
    this.isLoading = false;
  }

  getCities() {
    this.cityService.loadCities()
      .subscribe(cities => this.cities = cities);
  }

  onChange(event: any) {
    this.files = event.target.files;
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.createFieldFormGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  createField() {
    this.isLoaded = false;
    this.isLoading = true;

    const {
      cityId,
      streetName,
      zipCode,
      images
    } = this.createFieldFormGroup.value;

    const body = new FormData();
    body.append('cityId', cityId);
    body.append('streetName', streetName);
    body.append('zipCode', zipCode);

    for (let index = 0; index < this.files.length; index++) {
      body.append('images', this.files[index]);
    }

    this.fieldService.createField(body)
      .subscribe({
        next: (res: any) => {
          this.toastr.success("Успешно добавихте ново бойно поле");
          this.isLoaded = true;
          this.isLoading = false;

          this.router.navigate([`/fields/${res.id}`])
        },
        complete: () => {
          this.isLoaded = true;
          this.isLoading = false;
        }
      })
  }
}
