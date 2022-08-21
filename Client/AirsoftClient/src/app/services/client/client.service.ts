import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClientInputModel } from 'src/app/models/client/clientInputModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(private httpClient: HttpClient) { }

  register(body: ClientInputModel): Observable<ClientInputModel> {
    return this.httpClient.post<ClientInputModel>(`${environment.apiUrl}/clients`, body);
  }

  // getClientData(): Observable<UserClientViewModel> {
  //   return this.httpClient.get<UserClientViewModel>(`${environment.apiUrl}/client/profile`);
  // }

  // edit(body: EditClientModel): Observable<Object> {
  //   return this.httpClient.put(`${environment.apiUrl}/client/edit`, body);
  // }
}
