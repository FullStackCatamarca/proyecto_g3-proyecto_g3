using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MisViajes.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Domicilio { get; set; }
        public Nullable<int> Pais { get; set; }
        public Nullable<int> Provincia { get; set; }
        public Nullable<int> Departamento { get; set; }
        public string FechaNacimiento { get; set; }
        public Nullable<int> Sexo { get; set; }
        public string AvatarUrl { get; set; }
        public string Posicion { get; set; }
        public string Descripcion { get; set; }
        public string CodigoPostal { get; set; }
        public string Acerca { get; set; }
        public string ImgUrl { get; set; }

        public ICollection<Rutas> Ruta { get; set; }
        public ICollection<Temas> Tema { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            userIdentity.AddClaim(new Claim("FullName", this.Apellido + " " + this.Nombre));
            userIdentity.AddClaim(new Claim("ImgUrl", (this.ImgUrl != null) ? this.ImgUrl : "../assets/img/dashboard/profile-img-01.jpg"));
            userIdentity.AddClaim(new Claim("AvatarUrl", (this.AvatarUrl != null) ? this.AvatarUrl : string.Empty));
            
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MisViajes.Models.Slides> Slides { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.UsuarioModel> UsuarioModels { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Paises> Paises { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Provincias> Provincias { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Departamentos> Departamentos { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Localidades> Localidades { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Rutas> Rutas { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Servicios> Servicios { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Waypoint> Waypoints { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Posts> Posts { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Temas> Temas { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Votos> Votos { get; set; }

        public System.Data.Entity.DbSet<MisViajes.Models.Respuestas> Respuestas { get; set; }
        public object Hospedajes { get; internal set; }
    }
}