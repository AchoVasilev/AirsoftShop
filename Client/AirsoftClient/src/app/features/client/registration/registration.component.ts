import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CityViewModel } from 'src/app/models/address/cityViewModel';
import { ClientInputModel } from 'src/app/models/client/clientInputModel';
import { AuthService } from 'src/app/services/auth/auth.service';
import { CityService } from 'src/app/services/city/city.service';
import { ClientService } from 'src/app/services/client/client.service';
import { passwordMatch } from 'src/app/shared/utils';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  cities: CityViewModel[] = [];
  passwordControl = new FormControl('', [Validators.required, Validators.minLength(6)]);

  get passwordsGroup(): FormGroup {
    return this.registerFormGroup.controls['passwords'] as FormGroup;
  }

  registerFormGroup: FormGroup = this.formBuilder.group({
    'firstName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'lastName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'streetName': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'cityName': new FormControl('', [Validators.required]),
    'phoneNumber': new FormControl('', [Validators.required, Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")]),
    'email': new FormControl('', [Validators.required, Validators.email]),
    'username': new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(40)]),
    'passwords': new FormGroup({
      'password': this.passwordControl,
      'repeatPassword': new FormControl('', [passwordMatch(this.passwordControl)])
    })
  })

  constructor(
    private formBuilder: FormBuilder,
    private cityService: CityService,
    private authService: AuthService,
    private clientService: ClientService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    } else {
      this.loadCities();
    }
  }

  loadCities(): void {
    this.cityService.loadCities()
      .subscribe(c => this.cities = c);
  }

  shouldShowErrorForControl(controlName: string, sourceGroup: FormGroup = this.registerFormGroup) {
    return sourceGroup.controls[controlName].touched && sourceGroup.controls[controlName].invalid;
  }

  handleRegistration() {
    const { firstName, lastName, streetName, cityName, phoneNumber, email, username, passwords } = this.registerFormGroup.value;

    const body: ClientInputModel = {
      firstName,
      lastName,
      streetName,
      cityName,
      phoneNumber,
      email,
      username,
      password: passwords.password
    };

    this.clientService.register(body)
      .subscribe(() => this.router.navigate(['/login']));
  }
}
