import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Property } from '../Shared/Models/property';
import { Observable } from 'rxjs';
import { DataResponse } from '../Shared/Models/common';

@Injectable({
  providedIn: 'root',
})
export class PropertyService {
  private apiUrl: string = 'https://localhost:7074/api/Property/items';

  constructor(private http: HttpClient) {}

  getProperties(
    sortBy: string | null,
    lt: number | null,
    gt: number | null,
    pageIndex: number | null,
    pageSize: number | null
  ): Observable<DataResponse<Property>> {
    const params = new HttpParams()
      .set('sortBy', sortBy || 'Title')
      .set('lt', lt?.toString() || '100000')
      .set('gt', gt?.toString() || '0')
      .set('pageIndex', pageIndex?.toString() || '0')
      .set('pageSize', pageSize?.toString() || '10');

    return this.http.get<DataResponse<Property>>(this.apiUrl, {
      params,
    });
  }
  getPropertiesByLabel(label: string): Observable<DataResponse<Property>> {
    return this.http.get<DataResponse<Property>>(
      `${this.apiUrl}/label/${label}`
    );
  }

  getProperty(id: string): Observable<Property | null> {
    return this.http.get<Property | null>(`${this.apiUrl}/${id}`);
  }
}
