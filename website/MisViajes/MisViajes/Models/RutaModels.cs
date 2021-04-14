using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class Rutas
    {
        [Required]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(200)]
        public string Nombre { get; set; }
        public bool Abierto { get; set; }
        public bool Publico { get; set; }
        public bool Aprobado { get; set; }
        public Servicios Servicio { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

    }
}