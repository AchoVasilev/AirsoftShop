import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginInputModel } from 'src/app/models/loginInputModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  loginUrl: string = `${environment.apiUrl}/users/login`;

  constructor(private httpClient: HttpClient) { }

  login(body: LoginInputModel): Observable<any> {
    return this.httpClient.post(this.loginUrl, body);
  }


}
