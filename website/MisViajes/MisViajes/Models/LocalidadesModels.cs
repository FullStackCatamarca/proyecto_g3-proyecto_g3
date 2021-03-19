namespace MisViajes.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Localidades
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public int DepartamentoId { get; set; }
        public virtual Departamentos Departamento { get; set; }
    }
}
