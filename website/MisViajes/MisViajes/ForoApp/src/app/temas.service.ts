import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Temas } from './temas';

@Injectable({
  providedIn: 'root'
})
export class TemasService {

  private REST_API_SERVER = "https://localhost:44344/api/TemasWebApi";

  constructor(private httpClient: HttpClient) { }

  public sendGetRequest() {
    return this.httpClient.get<Temas[]>(this.REST_API_SERVER);
  }
}
