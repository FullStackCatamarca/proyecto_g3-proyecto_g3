//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MisViajes.Usuario
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Domicilio { get; set; }
        public Nullable<int> Pais { get; set; }
        public Nullable<int> Provincia { get; set; }
        public Nullable<int> Departamento { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public Nullable<int> Sexo { get; set; }
        public string AspNetUser { get; set; }
        public string AvatarUrl { get; set; }
        public string Posicion { get; set; }
        public string Descripcion { get; set; }
        public string CodigoPostal { get; set; }
        public string Acerca { get; set; }
        public string ImgUrl { get; set; }
    }
}
