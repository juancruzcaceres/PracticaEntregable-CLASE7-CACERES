using Entidades;
using EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public sealed class Principal
    {
        private readonly static Principal _instance = new Principal();
        public static Principal Instance { get { return _instance; } }

        private Principal()
        {
            Productos = new List<Elemento>();
            CantidadPantallas = 0;
            CantidadComputadoras = 0;
        }

        public EventHandler<AgregarEliminarProductoEventArgs> eventoAgregarEliminarProducto;

        public List<Elemento> Productos { get; set; }
        public int CantidadPantallas { get; set; }
        public int CantidadComputadoras { get; set; }

        //Agregar un producto 'Pantalla' a la lista Productos
        public void AgregarProducto(string modelo, string marca, string nroDeSerie, int añoFabricacion, int? pulgadas)
        {
            Pantalla producto = new Pantalla()
            {
                Modelo = modelo,
                Marca = marca,
                NroDeSerie = nroDeSerie,
                AñoFabricacion = añoFabricacion,
                Pulgadas = pulgadas
            };
            CantidadPantallas++;
            Productos.Add(producto);
            this.eventoAgregarEliminarProducto(this, new AgregarEliminarProductoEventArgs() { TipoDeProducto = "Pantalla", ID = producto.ID});
        }

        //Agregar un producto 'Computadora' a la lista Productos
        public void AgregarProducto(string modelo, string marca, string nroDeSerie, string descripcionDelProcesador, int cantidadRam, string fabricante)
        {
            Computadora producto = new Computadora()
            {
                Modelo = modelo,
                Marca = marca,
                NroDeSerie = nroDeSerie,
                DescripcionDelProcesador = descripcionDelProcesador,
                CantidadRAM = (Computadora.TipoMemoriaRam)Enum.ToObject(typeof(Computadora.TipoMemoriaRam), cantidadRam),
                Fabricante = fabricante
            };
            CantidadComputadoras++;
            Productos.Add(producto);
            this.eventoAgregarEliminarProducto(this, new AgregarEliminarProductoEventArgs() { TipoDeProducto = "Computadora", ID = producto.ID });
        }

        //Eliminar un producto de la lista Productos
        public void EliminarProducto(string id)
        {
            if (Productos.Exists(x => x.ID == id))
            {
                Elemento producto = Productos.Find(x => x.ID == id);
                
                if (producto is Pantalla)
                    CantidadPantallas--;
                else
                    CantidadComputadoras--;

                Productos.Remove(producto);
                this.eventoAgregarEliminarProducto(this, new AgregarEliminarProductoEventArgs() { TipoDeProducto = producto is Pantalla? "Pantalla" : "Computadora", ID = producto.ID });
            }
        }

        //Retorna las listas de descripciones para Pantallas y Computadoras
        public List<List<string>> ObtenerDescripcionesDeProductos()
        {
            List<List<string>> descripciones = new List<List<string>>();
            List<string> descripcionesPantallas = new List<string>();
            List<string> descripcionesComputadoras = new List<string>();

            foreach (Elemento producto in Productos)
            {
                if (producto is Pantalla)
                {
                    Pantalla productoPantalla = producto as Pantalla;
                    descripcionesPantallas.Add($"MONITOR {productoPantalla.Marca} - {productoPantalla.Modelo} {productoPantalla.Pulgadas}‘’");
                }
                else
                {
                    Computadora productoComputadora = producto as Computadora;
                    descripcionesComputadoras.Add($"PC {productoComputadora.Modelo} - {productoComputadora.Marca} - {productoComputadora.DescripcionDelProcesador} {productoComputadora.CantidadRAM} RAM - {productoComputadora.Fabricante}");
                }
            }
            descripciones.Add(descripcionesPantallas);
            descripciones.Add(descripcionesComputadoras);
            return descripciones;
        }

        //Obtener lista de objetos del mismo tipo que contenga las dos listas ordenadas por tipo de producto
        //¿Cual seria el criterio para ordenar por tipo de producto?


    }
}
