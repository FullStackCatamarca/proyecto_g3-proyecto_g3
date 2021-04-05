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

    public class Respuestas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public Posts Post { get; set; }
        public ApplicationUser User { get; set; }

    }

    public class Votos
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public bool Up { get; set; }
        public Posts Post { get; set; }
        public ApplicationUser User { get; set; }

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
        public ApiPosts[] Posts { get; set; }


        public static explicit operator ApiTemas(Temas t)
        {

            ApiTemas at = new ApiTemas();
            at.Id = t.Id;
            at.Nombre = t.Nombre;
            at.Fecha = t.Fecha;
            at.Descripcion = t.Descripcion;
            at.Activo = t.Activo;
            at.Usuario = t.User.UserName;
            at.AvatarUrl = (t.User.AvatarUrl != null) ? t.User.AvatarUrl : "../assets/img/dashboard/profile-img-01.jpg";
            at.Respuestas = 0;
            return at;
        }
    }

    public class ApiPosts
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public ApiRespuestas[] Respuesta { get; set; }
        public int? VotosUp { get; set; }
        public int? VotosDown { get; set; }
        public int? Respuestas { get; set; }
        public string AvatarUrl { get; set; }



        public static explicit operator ApiPosts(Posts p)
        {
            ApiPosts Ap = new ApiPosts();
            Ap.Id = p.Id;
            Ap.Descripcion = p.Descripcion;
            Ap.Fecha = p.Fecha;
            Ap.Usuario = p.User.UserName;
            Ap.AvatarUrl = (p.User.AvatarUrl != null) ? p.User.AvatarUrl : "../assets/img/dashboard/profile-img-01.jpg";
            Ap.Respuestas = 0;
            Ap.VotosDown = 0;
            Ap.VotosUp = 0;

            return Ap;
        }
    }

    public class ApiRespuestas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public string AvatarUrl { get; set; }

        public static explicit operator ApiRespuestas(Respuestas r)
        {
            ApiRespuestas Ar = new ApiRespuestas();
            Ar.Id = r.Id;
            Ar.Descripcion = r.Descripcion;
            Ar.Fecha = r.Fecha;
            Ar.Descripcion = r.Descripcion;
            Ar.Usuario = r.User.UserName;
            Ar.AvatarUrl = (r.User.AvatarUrl != null) ? r.User.AvatarUrl : "../assets/img/dashboard/profile-img-01.jpg";

            return Ar;

        }
    }

    

    

}