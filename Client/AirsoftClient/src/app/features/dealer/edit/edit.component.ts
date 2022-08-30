import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityViewModel } from 'src/app/models/address/cityViewModel';
import { EditDealerModel } from 'src/app/models/dealers/editDealerModel';
import { UserDealerViewModel } from 'src/app/models/dealers/userDealerViewModel';
import { CityService } from 'src/app/services/city/city.service';
import { DealerService } from 'src/app/services/dealer/dealer.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  dealer: UserDealerViewModel | undefined;
  cities: CityViewModel[] = [];
  isLoading: boolean = true;
  isLoaded: boolean = false;

  editFormGroup: FormGroup = this.formBuilder.group({
    'name': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'dealerNumber': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'streetName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'siteUrl': new FormControl(''),
    'cityName': new FormControl('', [Validators.required]),
    'phone': new FormControl('', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
    'email': new FormControl('', [Validators.required, Validators.email]),
  });
  constructor(
    private formBuilder: FormBuilder,
    private cityService: CityService,
    private dealerService: DealerService,
    private router: Router) { }

  ngOnInit(): void {
    this.getDealerData();
    this.loadCities();

    this.editFormGroup.patchValue({
      name: this.dealer?.dealer.name,
      dealerNumber: this.dealer?.dealer.dealerNumber,
      username: this.dealer?.username,
      streetName: this.dealer?.dealer.address.streetName,
      cityName: this.dealer?.dealer.address.city.name,
      phone: this.dealer?.dealer.phoneNumber,
      email: this.dealer?.email,
      siteUrl: this.dealer?.dealer.siteUrl
    });

    this.isLoaded = true;
    this.isLoading = false;
  }

  getDealerData(): void {
    this.dealerService.getDealerData()
      .subscribe(dealerData => {
        this.dealer = dealerData;
      });
  }

  loadCities(): void {
    this.cityService.loadCities()
      .subscribe(c => this.cities = c);
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.editFormGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  editData() {
    const { name, dealerNumber, streetName, cityName, phone, email, siteUrl } = this.editFormGroup.value;
    const editModel: EditDealerModel = {
      name,
      dealerNumber,
      streetName,
      cityName,
      phone,
      email,
      siteUrl
    };

    this.dealerService.edit(editModel)
      .subscribe({
        next: (res: any) => {
          this.editFormGroup.reset();
          this.router.navigate(['/dealer/profile']);
        }
      })
  }

  closeEdit() {
    this.editFormGroup.reset();
    this.router.navigate(['/client/profile']);
  }
}
