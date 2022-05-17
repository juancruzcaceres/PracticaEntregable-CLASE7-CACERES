using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventArguments
{
    public class AgregarEliminarProductoEventArgs : EventArgs
    {
        public string TipoDeProducto { get; set; }
        public string ID { get; set; }
    }
}
