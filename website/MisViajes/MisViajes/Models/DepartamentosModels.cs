using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace MisViajes.Models
{
    public class Departamentos
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int ProvinciaId { get; set; }

        public virtual Provincias Provincia { get; set; }
    }
}
