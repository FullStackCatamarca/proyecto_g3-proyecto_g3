import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Respuestas } from './respuestas';

@Injectable({
  providedIn: 'root'
})
export class RespuestasService {

  private REST_API_SERVER = "https://localhost:44344/api/RespuestasWebApi";

  constructor(private httpClient: HttpClient) { }

  public sendGetRequest() {
    return this.httpClient.get<Respuestas[]>(this.REST_API_SERVER);
  }
}
