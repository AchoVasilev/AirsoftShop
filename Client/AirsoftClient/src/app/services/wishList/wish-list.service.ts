import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WishListModel } from 'src/app/models/wishList/WishListModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WishListService {
  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  getItems(): Observable<WishListModel[]> {
    return this.httpClient.get<WishListModel[]>(`${this.apiUrl}/wishlists`);
  }

  addItem(id: string): Observable<any> {
    return this.httpClient.post(`${this.apiUrl}/wishlists`, {
      id
    });
  }

  removeItem(id: string): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/wishlists`, {
      body: id
    });
  }

  removeItems(ids: string[]): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/wishlists/bulkRemove`, {
      body: ids
    });
  }
}
