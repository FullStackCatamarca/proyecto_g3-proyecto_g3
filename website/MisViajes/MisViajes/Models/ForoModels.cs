using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class Temas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class Posts
    {
        // ToDo: Id User
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Temas Tema { get; set; }
    }

    public class Votos
    {
        public int Voto { get; set; }
        public DateTime Fecha { get; set; }
        public bool Up { get; set; }
        public Posts Post { get; set; }

    }
    public class Respuesta : Posts
    {

    }
}