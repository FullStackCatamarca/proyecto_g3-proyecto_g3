using System;
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
    }
}