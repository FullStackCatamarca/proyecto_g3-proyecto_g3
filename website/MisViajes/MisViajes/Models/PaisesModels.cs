using System;
using System.Collections.Generic;

namespace MisViajes.Models
{
  
    public class Paises
    {
        
        public Paises()
        {
            this.Provincias = new HashSet<Provincias>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
    

        public virtual ICollection<Provincias> Provincias { get; set; }
    }
}
