import { Respuestas } from './respuestas';

export interface Posts {
  Id: number;
  Usuario: string;
  AvatarUrl: string;
  Descripcion: string;
  Respuestas: number;
  VotosUp: number;
  VotosDown: number;
  Fecha: Date;
  Respuesta: Array<Respuestas>;
}
