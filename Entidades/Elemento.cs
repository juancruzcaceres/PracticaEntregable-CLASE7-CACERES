using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Elemento
    {
        public string ID { get { return Modelo + Marca + NroDeSerie; } }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string NroDeSerie { get; set; }
    }
}
