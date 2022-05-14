using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Computadora : Elemento
    {
        public string DescripcionDelProcesador { get; set; }
        public TipoMemoriaRam CantidadRAM { get; set; }
        public string Fabricante { get; set; }


        public enum TipoMemoriaRam
        {
            GB2,
            GB4,
            GB8,
            GB16
        }
    }
}
