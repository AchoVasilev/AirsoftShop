import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  loadCategories() {
    return this.httpClient.get<CategoryViewModel[]>(`${this.apiUrl}/categories/all`);
  }

  loadNewestCategories() {
    return this.httpClient.get<BasicCategoryViewModel[]>(`${this.apiUrl}/categories/newest`);
  }

  loadGunSubcategories() {
    return this.httpClient.get<GunSubCategoryViewModel[]>(`${this.apiUrl}/categories/gunSubcategories`);
  }
}
