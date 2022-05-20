using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Entidades;
using EventArguments;

namespace Persistencia
{
    public class Datos
    {
        public string PathProductos { get; set; }
        public string PathPantallas { get; set; }
        public string PathComputadoras { get; set; }

        public Datos()
        {
            PathProductos = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PathProductos += @"\Productos.txt";
            PathPantallas = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PathPantallas += @"\Pantallas.txt";
            PathComputadoras = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            PathComputadoras += @"\Computadoras.txt";
        }

        public void VerificarArchivos()
        {
            if (!File.Exists(PathProductos))
                File.Create(PathProductos).Close();

            if (File.Exists(PathPantallas))
                LeerPantallas();
            else
                File.Create(PathPantallas).Close();
            
            if (File.Exists(PathComputadoras))
                LeerComputadoras();
            else
                File.Create(PathComputadoras).Close();
        }

        public List<Pantalla> LeerPantallas()
        {
            using(StreamReader reader = new StreamReader(PathPantallas))
            {
                string contenido = reader.ReadToEnd();
                List<Pantalla> resultado = new List<Pantalla>();
                if (contenido!="" && contenido!= "null")
                    resultado = JsonConvert.DeserializeObject<List<Pantalla>>(contenido);
                return resultado;
            }
        }

        public bool GuardarPantallas(List<Pantalla>Pantallas)
        {
            using(StreamWriter writer = new StreamWriter(PathPantallas, false))
            {
                writer.Write(JsonConvert.SerializeObject(Pantallas));
                return true;
            }
        }

        public List<Computadora> LeerComputadoras()
        {
            using (StreamReader reader = new StreamReader(PathComputadoras))
            {
                string contenido = reader.ReadToEnd();
                List<Computadora> resultado = new List<Computadora>();
                if (contenido != "" && contenido != "null")
                    resultado = JsonConvert.DeserializeObject<List<Computadora>>(contenido);
                return resultado;
            }
        }

        public bool GuardarComputadoras(List<Computadora> Computadoras)
        {
            using (StreamWriter writer = new StreamWriter(PathComputadoras, false))
            {
                writer.Write(JsonConvert.SerializeObject(Computadoras));
                return true;
            }
        }

    }
}
