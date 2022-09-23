import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClothingService {
  private apiUrl: string = `${environment.apiUrl}/clothings`;

  constructor(private httpClient: HttpClient) { }

  createClothing(body: FormData): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}`, body);
  }
}
