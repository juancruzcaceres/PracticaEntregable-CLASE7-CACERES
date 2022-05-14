using Entidades;
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

        public List<Elemento> Productos { get; set; }

        //Agregar un producto Pantalla a la lista Productos
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
            Productos.Add(producto);
        }

        //Agregar un producto Computadora a la lista Productos
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
            Productos.Add(producto);
        }

        //Eliminar un producto de la lista Productos
        public void EliminarProducto(string id)
        {
            if (Productos.Exists(x => x.ID == id))
                Productos.Remove(Productos.Find(x=>x.ID==id));
        }

        //Obtener descripcion de un producto
        public string ObtenerDescripcion()
        {
            return "";
        }

        //Obtener lista de objetos del mismo tipo que contenga las dos listas ordenadas por tipo de producto




    }
}
