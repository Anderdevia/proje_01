using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public class Zona
    {
        // Instancia del acceso a datos para la capa de datos
        Proyecto.DA.MP_Zona MP = new DA.MP_Zona();

        public List<BE.Zona> Listar()
        {
            // Llama al método listar() de la capa de datos para recuperar las zonas
            return MP.listar();
        }
    }
}
