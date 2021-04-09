import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Temas } from './temas';

@Injectable({
  providedIn: 'root'
})
export class TemasService {

  private REST_API_SERVER_BASE = "https://localhost:44344/api/TemasWebApi";


  constructor(private httpClient: HttpClient) { }

  public sendGetRequest(tipo: string) {
    if (tipo == "0" || tipo == "1" || tipo == "2")
      return this.httpClient.get<Temas[]>(this.REST_API_SERVER_BASE + "?order=" + tipo);

    return this.httpClient.get<Temas[]>(this.REST_API_SERVER_BASE);
  }

  public sendFindRequest(tipo: string) {
    if (tipo == "0" || tipo == "1" || tipo == "2")
      return this.httpClient.get<Temas[]>(this.REST_API_SERVER_BASE + "?order=" + tipo);

    return this.httpClient.get<Temas[]>(this.REST_API_SERVER_BASE);
  }
}
