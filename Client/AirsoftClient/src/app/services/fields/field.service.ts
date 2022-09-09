import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FieldDetailsModel } from 'src/app/models/fields/fieldDetailsModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FieldService {

  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  createField(body: FormData): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}/fields`, body);
  }

  detailsById(id: number): Observable<FieldDetailsModel> {
    return this.httpClient.get<FieldDetailsModel>(`${this.apiUrl}/fields/${id}`);
  }

  deleteById(id: number): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/fields/${id}`);
  }
}
