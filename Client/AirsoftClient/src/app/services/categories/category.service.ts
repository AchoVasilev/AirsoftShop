import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BasicCategoryViewModel } from 'src/app/models/categories/basicCategoryViewModel';
import { CategoryViewModel } from 'src/app/models/categories/categoryViewModel';
import { SubCategoryViewModel } from 'src/app/models/categories/subCategoryViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = environment.apiUrl + '/categories';
  constructor(private httpClient: HttpClient) { }

  loadCategories(): Observable<CategoryViewModel[]> {
    return this.httpClient.get<CategoryViewModel[]>(`${this.apiUrl}/all`);
  }

  loadNewestCategories(): Observable<BasicCategoryViewModel[]> {
    return this.httpClient.get<BasicCategoryViewModel[]>(`${this.apiUrl}/newest`);
  }

  loadGunSubcategories(): Observable<SubCategoryViewModel[]> {
    return this.httpClient.get<SubCategoryViewModel[]>(`${this.apiUrl}/gunSubcategories`);
  }

  loadClothingSubcategories(): Observable<SubCategoryViewModel[]> {
    return this.httpClient.get<SubCategoryViewModel[]>(`${this.apiUrl}/clothingSubcategories`);
  }
}
