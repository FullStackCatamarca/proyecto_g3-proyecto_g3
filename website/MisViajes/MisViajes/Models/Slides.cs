using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class Slides
    {
        public int id { get; set; }
        public string UrlImagen { get; set; }
        public string Encabezado { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UrlDestino { get; set; }
        public string TxtBoton { get; set; }
        public bool Habilitado { get; set; }
    }
}