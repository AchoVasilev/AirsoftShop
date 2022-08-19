import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginInputModel } from 'src/app/models/loginInputModel';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginFormGroup: FormGroup = this.formBuilder.group({
    'email': new FormControl('', [Validators.required, Validators.email]),
    'password': new FormControl('', [Validators.required, Validators.minLength(6)])
  });

  constructor(private formBuilder: FormBuilder, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/']);
    }
  }

  loginHandler() {
    const { email, password } = this.loginFormGroup.value;
    const body: LoginInputModel = {
      email,
      password
    };

    this.authService.login(body)
      .subscribe(result => {
        this.authService.setToken(result['token']);

        if (result['isClient']) {
          this.authService.setClient(result['isClient']);
        }
      });
  }
}
