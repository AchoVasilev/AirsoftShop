import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DealerGunsList } from 'src/app/models/products/guns/dealerGunList';
import { GunDetailsViewModel } from 'src/app/models/products/guns/gunDetailsViewModel';
import { GunEditModel } from 'src/app/models/products/guns/gunEditModel';
import { GunsViewModel } from 'src/app/models/products/guns/gunsViewModel';
import { InitialGunViewModel } from 'src/app/models/products/guns/initialGunViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GunService {
  private apiUrl: string = `${environment.apiUrl}/guns`;

  constructor(private httpClient: HttpClient) { }

  createGun(body: FormData): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}`, body);
  }

  getNewestEightGuns(): Observable<InitialGunViewModel[]> {
    return this.httpClient.get<InitialGunViewModel[]>(`${this.apiUrl}/newest`);
  }

  getGunDetails(gunId: string): Observable<GunDetailsViewModel> {
    return this.httpClient.get<GunDetailsViewModel>(`${this.apiUrl}/${gunId}`)
  }

  deleteGun(gunId: string): Observable<object> {
    return this.httpClient.delete(`${this.apiUrl}`, {
      params: {
        gunId
      }
    });
  }

  editGun(model: GunEditModel): Observable<object> {
    return this.httpClient.put(`${this.apiUrl}`, model);
  }

  getAllGunsQuery(categoryName: string,
    itemsPerPage: number,
    orderBy: string, dealers: string[], manufacturers: string[], colors: string[], powers: number[], page: number)
    : Observable<GunsViewModel> {
    return this.httpClient.get<GunsViewModel>(`${this.apiUrl}`, {
      params: {
        categoryName,
        itemsPerPage,
        orderBy,
        dealers,
        manufacturers,
        colors,
        powers,
        page
      }
    })
  }

  getDealerProducts(): Observable<DealerGunsList[]> {
    return this.httpClient.get<DealerGunsList[]>(`${this.apiUrl}/mine`);
  }
}
