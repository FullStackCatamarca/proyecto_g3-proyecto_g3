using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class Waypoint
    {
        [Required]
        public int Id { get; set; }
        public int rutaId { get; set; }
        public virtual Rutas ruta { get; set; }
        public int ServiciosId { get; set; }
        public virtual Servicios Servicios { get; set; }

    }
}