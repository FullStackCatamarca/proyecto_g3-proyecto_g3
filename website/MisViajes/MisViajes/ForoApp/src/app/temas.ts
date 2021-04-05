import { Posts } from './posts';


export interface Temas {
  Id: number;
  Usuario: string;
  AvatarUrl: string;
  Titulo: string;
  Descripcion: string;
  Activo: boolean;
  Respuestas: number;
  VotosUp: number;
  VotosDown: number;
  Nombre: string;
  Fecha: Date;
  Posts: Array<Posts>;
}
