import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDealerViewModel } from 'src/app/models/dealers/UserDealerViewModel';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DealerService {
  private apiUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  register(body: FormData) {
    return this.httpClient.post(`${this.apiUrl}/dealers`, body, { withCredentials: true });
  }

  getDealerData(): Observable<UserDealerViewModel> {
    return this.httpClient.get<UserDealerViewModel>(`${this.apiUrl}/dealer/profile`);
  }
}
