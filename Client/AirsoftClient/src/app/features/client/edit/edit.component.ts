import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityViewModel } from 'src/app/models/address/cityViewModel';
import { EditClientModel } from 'src/app/models/client/editClientModel';
import { UserClientViewModel } from 'src/app/models/client/userClientViewModel';
import { CityService } from 'src/app/services/city/city.service';
import { ClientService } from 'src/app/services/client/client.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  client: UserClientViewModel | undefined;
  cities: CityViewModel[] = [];
  isLoading: boolean = true;
  isLoaded: boolean = false;

  editFormGroup: FormGroup = this.formBuilder.group({
    'firstName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'lastName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'streetName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'cityName': new FormControl('', [Validators.required]),
    'phoneNumber': new FormControl('', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
    'email': new FormControl('', [Validators.required, Validators.email]),
  });

  constructor(
    private clientService: ClientService,
    private cityService: CityService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getClientData();
    this.loadCities();
    this.isLoaded = true;
    this.isLoading = false;

    this.editFormGroup.patchValue({
      firstName: this.client!.client.firstName,
      lastName: this.client!.client.lastName,
      streetName: this.client!.client.address.streetName,
      cityName: this.client!.client.address.city.name,
      phoneNumber: this.client!.client.phoneNumber,
      email: this.client!.email
    });
  }

  getClientData(): void {
    this.clientService.getClientData()
      .subscribe(clientData => {
        this.client = clientData;
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
    const { firstName, lastName, streetName, cityName, phoneNumber, email } = this.editFormGroup.value;
    const editModel: EditClientModel = {
      firstName,
      lastName,
      streetName,
      cityName,
      phoneNumber,
      email
    };

    this.clientService.edit(editModel)
      .subscribe({
        next: (res: any) => {
          this.editFormGroup.reset();
          this.router.navigate(['/client/profile']);
        }
      })
  }

  closeEdit() {
    this.editFormGroup.reset();
    this.router.navigate(['/client/profile']);
  }
}
