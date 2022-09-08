import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FieldService {

  private apiUrl: string = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  createField(body: FormData): Observable<any> {
    return this.httpClient.post(`${this.apiUrl}/fields`, body);
  }
}
