import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginInputModel } from 'src/app/models/loginInputModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  private loginUrl: string = `${this.baseUrl}/users/login`;

  constructor(private httpClient: HttpClient) { }

  login(body: LoginInputModel): Observable<any> {
    return this.httpClient.post(this.loginUrl, body);
  }

  setToken(token: any) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isAuthenticated() {
    if (this.getToken()) {
      return true;
    }

    return false;
  }

  setClient(isClient: any) {
    localStorage.setItem('isClient', isClient);
  }

  setDealer(isDealer: any) {
    localStorage.setItem('isDealer', isDealer);
  }

  getClient() {
    if (localStorage.getItem('isClient')) {
      return true;
    }

    return false;
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('isClient');
  }
}
