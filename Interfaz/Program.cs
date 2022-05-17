using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventArguments;
using Extensiones;

namespace Interfaz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Extensiones.Extensiones extensiones = new Extensiones.Extensiones();
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
                            if (Int32.TryParse(añoFabricacion, out Int32 numeroAñoFabricacion))
                            {
                                Console.WriteLine($"Output: {numeroAñoFabricacion}");
                            }
                            else
                            {
                                Console.WriteLine("Error, ingrese un año valido.");
                                goto iniAñoFabricacion;
                            }
                            iniPulgadas:
                            Console.WriteLine("Ingrese pulgadas: ");
                            string pulgadas = Console.ReadLine();
                            if (Int32.TryParse(pulgadas, out Int32 numeroDePulgadas))
                            {
                                Console.WriteLine($"Output: {numeroDePulgadas}");
                            }
                            else
                            {
                                Console.WriteLine("Error, ingrese un numero valido.");
                                goto iniPulgadas;
                            }
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
                            if (Int32.TryParse(cantidadRam, out Int32 numeroCantidadDeRam))
                            {
                                if (extensiones.NumeroEnRangoDeEnumRAM(numeroCantidadDeRam))
                                {
                                    //
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error, ingrese un numero valido.");
                                goto iniRam;
                            }
                            break;
                        default:
                            Console.WriteLine("Ingrese una opción valida.");
                            goto iniTipoElemento;
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Ingrese una opción valida.");
                    goto iniMenu;
                    break;
            }
            Console.ReadKey();
        }
    }
}
