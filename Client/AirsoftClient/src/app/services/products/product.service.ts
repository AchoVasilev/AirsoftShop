import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GunDetailsViewModel } from 'src/app/models/products/guns/gunDetailsViewModel';
import { InitialGunViewModel } from 'src/app/models/products/guns/initialGunViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  createGun(body: FormData): Observable<string> {
    return this.httpClient.post<string>(`${environment.apiUrl}/products`, body);
  }

  getNewestEightGuns(): Observable<InitialGunViewModel[]> {
    return this.httpClient.get<InitialGunViewModel[]>(`${this.apiUrl}/products/newest`);
  }

  getGunDetails(gunId: number): Observable<GunDetailsViewModel> {
    return this.httpClient.get<GunDetailsViewModel>(`${this.apiUrl}/products/details`, {
      params: {
        gunId
      }
    })
  }

  deleteGun(gunId: number): Observable<object> {
    return this.httpClient.delete(`${this.apiUrl}/products`, {
      params: {
        gunId
      }
    });
  }
}
