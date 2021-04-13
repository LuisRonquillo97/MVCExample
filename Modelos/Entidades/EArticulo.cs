using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Entidades
{
    public class EArticulo
    {

        public int ID { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public bool Eliminado { get; set; }
        
    }
}
