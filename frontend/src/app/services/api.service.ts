import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { DataResponseModel } from '../interface/data.interface';
export interface ApiStatusResponse {
  message: string;
  serverTime: string;
}

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) {}

  getStatus(): Observable<ApiStatusResponse> {
    return this.http.get<ApiStatusResponse>(`${environment.API_URL}status`);
  }

  getData(): Observable<DataResponseModel> {
    return this.http.get<DataResponseModel>(`${environment.API_URL}data/get-data`);
  }
}
