using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public abstract class Servicios
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public string Fotourl { get; set; }
        public string costo { get; set; }
        public string Puntuacion { get; set; }
        public string Localidad { get; set; }
        public string weburl { get; set; }
        public Nullable<bool> habilitado { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }


        public ICollection<Rutas> Rutas { get; set; }

    }

    public class Eventos : Servicios
    {
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Fin { get; set; }
    }

    public class Monumentos : Servicios
    {

    }

    public class Atracciones : Servicios
    {

    }

    public class Hospedajes : Servicios
    {

    }

    public class InheritanceMappingContext : DbContext
    {
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Posts> Post { get; set; }
    }


}