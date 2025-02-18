using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Proyecto.DA
{
    // Clase interna que maneja el acceso a la base de datos de la aplicación
    internal class AccesoDA
    {
        // Cadena de conexión obtenida del archivo de configuración
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexiondb"].ConnectionString;

        // Variable para manejar la conexión con la base de datos
        private SqlConnection conexion;

        // Método privado para abrir la conexión con la base de datos
        private void Abrir()
        {
            conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
        }

        // Método privado para cerrar la conexión con la base de datos
        private void Cerrar()
        {
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        // Método para crear un comando SQL con los parámetros proporcionados
        private SqlCommand CrearComando(string nombre, List<SqlParameter> parametros = null)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandText = nombre;
            comando.CommandType = CommandType.StoredProcedure;

            if (parametros != null && parametros.Count > 0)
            {
                comando.Parameters.AddRange(parametros.ToArray());
            }
            return comando;
        }

        // Método para ejecutar una consulta y obtener datos
        public DataTable Leer(string nombre, List<SqlParameter> parametros)
        {
            Abrir();

            DataTable tabla = new DataTable();
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = CrearComando(nombre, parametros);
            adaptador.Fill(tabla);

            adaptador = null;
            Cerrar();
            return tabla;
        }

        // Método para ejecutar una consulta que modifica datos
        public int Escribir(string nombre, List<SqlParameter> parametros)
        {
            int filafectada = 0;
            Abrir();
            SqlCommand comando = CrearComando(nombre, parametros);
            try
            {
                filafectada = comando.ExecuteNonQuery();
            }
            catch
            {
                filafectada = -1;
            }
            Cerrar();
            return filafectada;
        }

        // Método para crear un parámetro de SQL con el nombre y valor especificados
        public SqlParameter CrearParametro(string nombre, string valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombre;
            parametro.Value = valor;
            parametro.DbType = DbType.String;
            return parametro;
        }
    }
}
