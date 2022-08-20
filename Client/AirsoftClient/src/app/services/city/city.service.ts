import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CityViewModel } from 'src/app/models/address/cityViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  loadCities() {
    return this.http.get<CityViewModel[]>(`${this.apiUrl}/cities`);
  }
}
