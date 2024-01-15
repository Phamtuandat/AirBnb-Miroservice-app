import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataResponse, Label } from '../Models';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  constructor(private _http: HttpClient) {}
  baseUrl: string = 'https://localhost:7074/api';
  getAllLabels = (): Observable<Label[]> => {
    var response = this._http.get<Label[]>(`${this.baseUrl}/label`);
    return response;
  };
}
