using Entidades;
using EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia;

namespace Logica
{
    public sealed class Principal
    {
        private readonly static Principal _instance = new Principal();
        public static Principal Instance { get { return _instance; } }

        public EventHandler<AgregarEliminarProductoEventArgs> eventoAgregarEliminarProducto;

        //CUAL ES LA IDEA DE LAS 3 LISTAS?? CON UNA SOLA ES SUFICIENTE.
        public List<Elemento> Productos { get; set; }
        public List<Pantalla> Pantallas { get; set; }
        public List<Computadora> Computadoras { get; set; }

        public int CantidadPantallas { get; set; } //PORQUE NO USAR LISTA.COUNT PARA SABER LA CANTIDAD?
        public int CantidadComputadoras { get; set; } //PORQUE NO USAR LISTA.COUNT PARA SABER LA CANTIDAD?


        public Datos BaseDeDatos { get; set; }

        private Principal()
        {
            BaseDeDatos = new Datos();
            BaseDeDatos.VerificarArchivos();
            Pantallas = BaseDeDatos.LeerPantallas();
            Computadoras = BaseDeDatos.LeerComputadoras();
            Productos = new List<Elemento>();
            Productos = ObtenerProductos();
            CantidadPantallas = 0;
            CantidadComputadoras = 0;
        }

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
            Pantallas.Add(producto);
            Productos.Add(producto);
            BaseDeDatos.GuardarPantallas(Pantallas);
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
            Computadoras.Add(producto);
            Productos.Add(producto);
            BaseDeDatos.GuardarComputadoras(Computadoras);
            this.eventoAgregarEliminarProducto(this, new AgregarEliminarProductoEventArgs() { TipoDeProducto = "Computadora", ID = producto.ID });
        }

        //Eliminar un producto de la lista Productos
        public void EliminarProducto(string id)
        {
            if (Productos.Exists(x => x.ID == id))
            {
                Elemento producto = Productos.Find(x => x.ID == id);
                
                if (producto is Pantalla)
                {
                    CantidadPantallas--; //SI TE OLVIDAS O ELIMINAS ESTA LINEA EL CONTEO DA MAL SIEMPRE. EVITAR ESTAS VARIABLES
                    Pantallas.Remove(producto as Pantalla);
                    BaseDeDatos.GuardarPantallas(Pantallas);
                }
                else
                {
                    CantidadComputadoras--; //SI TE OLVIDAS O ELIMINAS ESTA LINEA EL CONTEO DA MAL SIEMPRE. EVITAR ESTAS VARIABLES
                    Computadoras.Remove(producto as Computadora);
                    BaseDeDatos.GuardarComputadoras(Computadoras);
                }
                Productos.Remove(producto);

                //PREGUNTAR SI ES != NULL
                this.eventoAgregarEliminarProducto(this, new AgregarEliminarProductoEventArgs() { TipoDeProducto = producto is Pantalla? "Pantalla" : "Computadora", ID = producto.ID });
            }
        }

        //Retorna las listas de descripciones para Pantallas y Computadoras
        public List<string> ObtenerDescripcionesDeProductos()
        {
            List<string> descripciones = new List<string>();

            foreach (Elemento producto in Productos)
            {
                if (producto is Pantalla)
                {
                    Pantalla productoPantalla = producto as Pantalla;
                    descripciones.Add($"· MONITOR {productoPantalla.Marca} - {productoPantalla.Modelo} {productoPantalla.Pulgadas}‘’");
                }
                else
                {
                    Computadora productoComputadora = producto as Computadora;
                    descripciones.Add($"· PC {productoComputadora.Modelo} - {productoComputadora.Marca} - {productoComputadora.DescripcionDelProcesador} {productoComputadora.CantidadRAM} RAM - {productoComputadora.Fabricante}");
                }
            }
            return descripciones;
        }

        //Obtener lista de objetos del mismo tipo que contenga las dos listas ordenadas por tipo de producto
        //¿Cual seria el criterio para ordenar por tipo de producto?
        public List<Elemento> ObtenerProductos()
        {
            List<Elemento> productos = new List<Elemento>();
            productos.AddRange(Pantallas);
            productos.AddRange(Computadoras);
            return productos;
        }

    }
}
