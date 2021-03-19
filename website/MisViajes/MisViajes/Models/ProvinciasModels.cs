using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MisViajes.Models
{
   
    public partial class Provincias
    { 
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int PaisId { get; set; }
        public virtual Paises Pais { get; set; }
    }
}
