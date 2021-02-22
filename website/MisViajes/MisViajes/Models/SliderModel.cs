using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MisViajes.Models
{
    public class SliderModel
    {
        public int ID { get; set; }
        public string PreTitulo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TextoBoton { get; set; }
        public string UrlBoton { get; set; }
        public string UrlImagen{ get; set; }
        public bool Activo { get; set; }
    }

}




