using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pantalla : Elemento
    {
        public int AñoFabricacion { get; set; }
        public bool EsNuevo { get { return AñoFabricacion == DateTime.Now.Year; } }
        public int? Pulgadas { get; set; }
    }
}
