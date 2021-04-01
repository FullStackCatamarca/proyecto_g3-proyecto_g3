using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public ApplicationUser User { get; set; }

    }

    public class Posts
    {

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Temas Tema { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Votos> Voto { get; set; }
    }

    public class Votos
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool Up { get; set; }
        public Posts Post { get; set; }
        public Respuestas Respuesta { get; set; }
        public ApplicationUser User { get; set; }

    }
    public class Respuestas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Posts Post { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Votos> Voto { get; set; }
    }

    public class ApiTemas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public string AvatarUrl { get; set; }
        public int? Respuestas { get; set; }
        public int? VotosUp { get; set; }
        public int? VotosDown { get; set; }


        public static explicit operator ApiTemas(Temas t)
        {

            ApiTemas at = new ApiTemas();
            at.Id = t.Id;
            at.Nombre = t.Nombre;
            at.Fecha = t.Fecha;
            at.Descripcion = t.Descripcion;
            at.Activo = t.Activo;
            at.Usuario = t.User.UserName;
            at.AvatarUrl = (t.User.AvatarUrl != null) ? t.User.AvatarUrl : "../assets/img/dashboard/profile-img-01.jpg"; ;
            at.Respuestas = 0;
            at.VotosUp = 0;
            at.VotosDown = 0;

            return at;
        }
    }
}