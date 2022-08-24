import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GunDetailsViewModel } from 'src/app/models/products/guns/gunDetailsViewModel';
import { GunEditModel } from 'src/app/models/products/guns/gunEditModel';
import { InitialGunViewModel } from 'src/app/models/products/guns/initialGunViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  createGun(body: FormData): Observable<string> {
    return this.httpClient.post<string>(`${this.apiUrl}/products`, body);
  }

  getNewestEightGuns(): Observable<InitialGunViewModel[]> {
    return this.httpClient.get<InitialGunViewModel[]>(`${this.apiUrl}/products/newest`);
  }

  getGunDetails(gunId: string): Observable<GunDetailsViewModel> {
    return this.httpClient.get<GunDetailsViewModel>(`${this.apiUrl}/products/details`, {
      params: {
        gunId
      }
    })
  }

  deleteGun(gunId: string): Observable<object> {
    return this.httpClient.delete(`${this.apiUrl}/products`, {
      params: {
        gunId
      }
    });
  }

  editGun(model: GunEditModel): Observable<object> {
    return this.httpClient.put(`${this.apiUrl}/products`, model);
  }
}
