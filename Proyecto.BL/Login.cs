using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
   public class Login
    {
        // Instancia del acceso a datos desde la capa de datos
        Proyecto.DA.MP_Login MP = new DA.MP_Login();


        // Método para guardar o actualizar un cliente
        public int Grabar(BE.Clientes clientes)
        {
            int res = 0;
            // Verifica si el ID del cliente es 0 (nuevo cliente)
            if (clientes.Id == 0)
            {
                // Llama al método Insertar() de la capa de datos para agregar el nuevo cliente
                res = MP.Insertar(clientes);
            }

            // Retorna el resultado de la operación (número de filas afectadas)
            return res;
        }

        // Método para acceder a la paguina principal
        public BE.Clientes AccederLogin(string correo, string contraseña)
        {
            // Llama al método Listar(id) de la capa de datos para recuperar un cliente específico
            return MP.Listar(correo,contraseña);
        }

        // Método para obtener el cliente específico basado en su ID
        public BE.Clientes VerificarAccederLogin(string correo, string contraseña)
        {
            // Llama al método Listar(id) de la capa de datos para recuperar un cliente específico
            return MP.VerificarAccederLogin(correo, contraseña);
        }
    }
}
