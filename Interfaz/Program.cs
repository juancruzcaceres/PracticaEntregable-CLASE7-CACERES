using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventArguments;
using Extensiones;
using Logica;

namespace Interfaz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Extensiones.Extensiones extensiones = new Extensiones.Extensiones();
            Principal principal = Principal.Instance; //ESTO NO ES NECESARIO. USAR SIEMPRE PRINCIPAL.INSTANCE EN TODOS LADOS
            principal.eventoAgregarEliminarProducto += handlerProductoAgregadoModificado;

            iniMenu:
            Console.WriteLine("1. Ingresar producto\n");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Ingrese modelo: ");
                    string modelo = Console.ReadLine();
                    Console.WriteLine("Ingrese marca: ");
                    string marca = Console.ReadLine();
                    Console.WriteLine("Ingrese número de serie: ");
                    string nroDeSerie = Console.ReadLine();
                    iniTipoElemento:
                    Console.WriteLine("1. Pantalla\n2. Computadora\n");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 1:
                            Console.Clear();
                            iniAñoFabricacion:
                            Console.WriteLine("Ingrese año de fabricacion: ");
                            string añoFabricacion = Console.ReadLine();
                            int añoDeFabricacion;
                            if (Int32.TryParse(añoFabricacion, out Int32 numeroAñoFabricacion))
                            {
                                añoDeFabricacion = numeroAñoFabricacion;
                            }
                            else
                            {
                                Console.WriteLine("Error, ingrese un año valido.");
                                goto iniAñoFabricacion;
                            }
                            iniPulgadas:
                            Console.WriteLine("Ingrese pulgadas: ");
                            string pulgadas = Console.ReadLine();
                            int numeroPulgadas;
                            if (Int32.TryParse(pulgadas, out Int32 numeroDePulgadas))
                            {
                                numeroPulgadas = numeroDePulgadas;
                            }
                            else
                            {
                                Console.WriteLine("Error, ingrese un numero valido.");
                                goto iniPulgadas;
                            }
                            principal.AgregarProducto(modelo, marca, nroDeSerie, añoDeFabricacion, numeroPulgadas);
                            break;
                        case 2:
                            Console.WriteLine("Ingrese una descripcion del procesador: ");
                            string descripcionDelProcesador = Console.ReadLine();
                            iniRam:
                            Console.WriteLine("Ingrese la cantidad de memoria RAM:\n" +
                                "0. 2GB\n" +
                                "1. 4GB\n" +
                                "2. 8GB\n" +
                                "3. 16GB");
                            string cantidadRam = Console.ReadLine();
                            int cantidadDeRAM;
                            if (Int32.TryParse(cantidadRam, out Int32 numeroCantidadDeRam))
                            {
                                if (extensiones.NumeroEnRangoDeEnumRAM(numeroCantidadDeRam))
                                    cantidadDeRAM = numeroCantidadDeRam;
                                else
                                {
                                    Console.WriteLine("Error, ingrese una cantidad de RAM válida.");
                                    goto iniRam;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error, ingrese un numero valido.");
                                goto iniRam;
                            }
                            Console.WriteLine("Ingrese fabricante: ");
                            string fabricante = Console.ReadLine();
                            principal.AgregarProducto(modelo, marca, nroDeSerie, descripcionDelProcesador, cantidadDeRAM, fabricante);
                            break;
                        default:
                            Console.WriteLine("Ingrese una opción valida.");
                            goto iniTipoElemento;
                    }
                    break;
                default:
                    Console.WriteLine("Ingrese una opción valida.");
                    goto iniMenu;
            }
            Console.ReadKey();
            Console.Clear();
            goto iniMenu;
        }

        static void handlerProductoAgregadoModificado(object sender, AgregarEliminarProductoEventArgs args)
        {
            Principal principal = Principal.Instance;

            //Imprime en pantalla la descripcion de cada producto de ambos tipos
            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Lista de productos: ");
            List<string> descripciones = principal.ObtenerDescripcionesDeProductos();
            foreach (string descripcion in descripciones)
            {
                if (descripcion == descripciones.Last())
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                    Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(descripcion);
            }

            //Imprime en pantalla el producto modificado y total de productos
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            Console.WriteLine($"Producto modificado '{args.TipoDeProducto}', ID '{args.ID}' - Total de Pantallas: {principal.CantidadPantallas}, Total de Computadoras: {principal.CantidadComputadoras} - Pantallas: {(principal.CantidadPantallas*100)/(principal.CantidadPantallas+principal.CantidadComputadoras)}% , Computadoras: {(principal.CantidadComputadoras*100)/(principal.CantidadPantallas + principal.CantidadComputadoras)}%");

        }
    }
}
