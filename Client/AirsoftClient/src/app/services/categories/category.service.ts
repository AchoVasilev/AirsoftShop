import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasicCategoryViewModel } from 'src/app/models/categories/basicCategoryViewModel';
import { CategoryViewModel } from 'src/app/models/categories/categoryViewModel';
import { GunSubCategoryViewModel } from 'src/app/models/categories/gunSubCategoryViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  loadCategories(): Observable<CategoryViewModel[]> {
    return this.httpClient.get<CategoryViewModel[]>(`${this.apiUrl}/categories/all`);
  }

  loadNewestCategories(): Observable<BasicCategoryViewModel[]> {
    return this.httpClient.get<BasicCategoryViewModel[]>(`${this.apiUrl}/categories/newest`);
  }

  loadGunSubcategories(): Observable<GunSubCategoryViewModel[]> {
    return this.httpClient.get<GunSubCategoryViewModel[]>(`${this.apiUrl}/categories/gunSubcategories`);
  }
}
