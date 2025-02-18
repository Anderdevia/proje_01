using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DA
{
    public class MP_Zona
    {
        private AccesoDA ac = new AccesoDA();

        // Método para listar todas las zonas
        public List<BE.Zona> listar()
        {
            // Utiliza el método Leer de AccesoDA para obtener los datos de todas las zonas
            DataTable tabla = ac.Leer("SP_Listar_Zona", null);
            List<BE.Zona> zonas = new List<BE.Zona>();

            // Itera sobre cada fila del DataTable y crea un objeto Zona para cada registro
            foreach (DataRow registro in tabla.Rows)
            {
                BE.Zona zona = new BE.Zona();
                zona.Id = int.Parse(registro["id"].ToString());
                zona.Nombre = registro["nombre"].ToString();
                zonas.Add(zona);
            }
            return zonas;
        }
    }
}
