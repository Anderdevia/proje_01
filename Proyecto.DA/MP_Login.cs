using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DA
{
    public class MP_Login
    {
        // Instancia del acceso a la base de datos general para esta clase
        private AccesoDA ac = new AccesoDA();

        // Método para obtener un cliente específico por su ID 
        public BE.Clientes Listar(string correo, string contraseña)
        {
            
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@usuario", correo));
            parameters.Add(ac.CrearParametro("@contraseña", contraseña));

            // Utiliza el método Leer de AccesoDA para obtener el cliente específico
            DataTable tabla = ac.Leer("SP_Acceder", parameters);
            BE.Clientes cliente = new BE.Clientes();

            if(tabla.Rows.Count >0)
            {
                // Crea un objeto cliente con los datos obtenidos del registro y lo retorna
                DataRow registro = tabla.Rows[0];
                cliente.Id = Convert.ToInt32(registro["ID"]);
                cliente.Contraseña = registro["contraseña"].ToString();
                cliente.Correo = registro["correo"].ToString();
                cliente.IdRol = (Rol)registro["idRol"];
                cliente.UsuarioLogin = registro["usuario"].ToString();
            }else
            {

            }

            return cliente;
        }

        // Método para obtener un cliente específico por su ID 
        public BE.Clientes VerificarAccederLogin(string correo, string contraseña)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@usuario", correo));
            parameters.Add(ac.CrearParametro("@contraseña", contraseña));

            // Utiliza el método Leer de AccesoDA para obtener el cliente específico
            DataTable tabla = ac.Leer("SP_Verificar_Acceder", parameters);
            BE.Clientes cliente = new BE.Clientes();

            if (tabla.Rows.Count > 0)
            {
                // Crea un objeto cliente con los datos obtenidos del registro y lo retorna
                DataRow registro = tabla.Rows[0];
                cliente.Id = Convert.ToInt32(registro["ID"]);
                cliente.Contraseña = registro["contraseña"].ToString();
                cliente.Correo = registro["correo"].ToString();
                cliente.IdRol = (Rol)registro["idRol"];
                cliente.UsuarioLogin = registro["usuario"].ToString();
            }
            else
            {

            }

            return cliente;
        }


        // Método para insertar un nuevo cliente
        public int Insertar(BE.Clientes clientes)
        {

            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(ac.CrearParametro("@nombre", clientes.Nombre));
            parameters.Add(ac.CrearParametro("@zona", clientes.Zona));
            parameters.Add(ac.CrearParametro("@usuario", clientes.UsuarioLogin));
            parameters.Add(ac.CrearParametro("@correo", clientes.Correo));
            parameters.Add(ac.CrearParametro("@contraseña", clientes.Contraseña));
            parameters.Add(ac.CrearParametro("@habeasData", clientes.HabeasData.ToString()));
            parameters.Add(ac.CrearParametro("@idrol", "1"));

            // Utiliza el método Escribir de AccesoDA para ejecutar la inserción y retorna el resultado
            return ac.Escribir("SP_Insertar_Clientes", parameters);
        }

    }
}
