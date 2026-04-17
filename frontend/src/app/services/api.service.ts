import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface ApiStatusResponse {
  message: string;
  serverTime: string;
}

@Injectable({ providedIn: 'root' })
export class ApiService {

  constructor(private http: HttpClient) {}

  getStatus(): Observable<ApiStatusResponse> {
    return this.http.get<ApiStatusResponse>('/api/status');
  }
}
