using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Windows.Forms.Design.AxImporter;

namespace Proyecto.DA
{
    public class MP_CentroOperaciones
    {
        private AccesoDA ac = new AccesoDA();

        // Método para listar todos los centros de operaciones
        public List<BE.CentroOperaciones> listar()
        {
            // Utiliza el método Leer de AccesoDA para obtener los datos de todos los centros de operaciones
            DataTable tabla = ac.Leer("SP_Listar_Centro_Operaciones", null);
            List<BE.CentroOperaciones> centroOperaciones = new List<BE.CentroOperaciones>();

            // Itera sobre cada fila del DataTable y crea un objeto CentroOperaciones para cada registro
            foreach (DataRow registro in tabla.Rows)
            {
                BE.CentroOperaciones centroOperacion = new BE.CentroOperaciones();
                centroOperacion.Codigo = int.Parse(registro["id"].ToString());
                centroOperacion.Nombre = registro["nombre"].ToString();
                centroOperacion.Descripcion = registro["descripcion"].ToString();
                centroOperacion.Direccion = registro["direccion"].ToString();
                centroOperacion.Telefono = registro["telefono"].ToString();
                centroOperacion.Correo = registro["correo"].ToString();
                centroOperacion.NombreImagen = registro["ruta_imagen"].ToString();
                centroOperaciones.Add(centroOperacion);
            }
            return centroOperaciones;
        }

        // Método para listar todos los clientes por fecha
        public List<BE.Visitas> ListaClientes(string fechaDesde, string fechaHasta, int opcion)
        {
            int contador = 1;

            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@fechaDesde", fechaDesde.ToString()));
            parameters.Add(ac.CrearParametro("@fechaHasta", fechaHasta.ToString()));
            parameters.Add(ac.CrearParametro("@opcion", opcion.ToString()));
            // Utiliza el método Leer de AccesoDA para obtener los datos de todos los Centro de Operaciones
            DataTable tabla = ac.Leer("SP_Lista_Clientes_Fecha", parameters);
            List<BE.Visitas> centroOperaciones = new List<BE.Visitas>();

            // Itera sobre cada fila del DataTable y crea un objeto Visitas para cada registro
            foreach (DataRow registro in tabla.Rows)
            {
                BE.Visitas centroOperacion = new BE.Visitas();
                centroOperacion.Id = contador++;
                centroOperacion.Nombre = registro["nombre_cliente"].ToString();
                centroOperacion.Id_centro_operacion = registro["nombre_centro"].ToString();
                if (DateTime.TryParse(registro["fecha"].ToString(), out DateTime fechaConvertida))
                {
                    centroOperacion.Fecha = fechaConvertida.ToString("dd/MM/yyyy"); 
                }
                centroOperacion.Visitas_aceptadas = registro["visitas_aceptadas"].ToString();
                centroOperaciones.Add(centroOperacion);
            }
            return centroOperaciones;
        }

        // Método para listar los menus
        public List<BE.MenuAdmin> listarMenu()
        {
            // Utiliza el método Leer de AccesoDA para obtener los datos de todos los modulos
            DataTable tabla = ac.Leer("SP_Modulos", null);
            List<BE.MenuAdmin> menus = new List<BE.MenuAdmin>();

            // Itera sobre cada fila del DataTable y crea un objeto MenuAdmin para mostrar
            foreach (DataRow registro in tabla.Rows)
            {
                BE.MenuAdmin menu = new BE.MenuAdmin();
                menu.Id = int.Parse(registro["id"].ToString());
                menu.Controlador = registro["controlador"].ToString();
                menu.Nombre = registro["nombre"].ToString();
                menu.Ver = int.Parse(registro["ver"].ToString());
    
                menus.Add(menu);
            }
            return menus;
        }


        // Método para la visita del usuario en el centro de operacion
        public List<BE.Visitas> verificaAccesoCentro(int id_usuario, int id_centro_operacion, int opcion)
        {
            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@id_usuario", id_usuario.ToString()));
            parameters.Add(ac.CrearParametro("@id_centro_operacion", id_centro_operacion.ToString()));
            parameters.Add(ac.CrearParametro("@opcion", opcion.ToString()));
            // Utiliza el método Leer de AccesoDA para obtener los datos del usuario ya visito este centro de operacion
            DataTable tabla = ac.Leer("SP_Listar_Visitas", parameters);
            List<BE.Visitas> visitas = new List<BE.Visitas>();

            // Itera sobre cada fila del DataTable y crea un objeto visitas para mostrar
            foreach (DataRow registro in tabla.Rows)
            {
                BE.Visitas visita = new BE.Visitas();
                visita.Nombre = registro["resultado"].ToString();

                visitas.Add(visita);
            }
            return visitas;
        }

        // Método para listar los menus el centro de operaciones
        public List<BE.MenuAdmin> ListarCentroOperaciones(string Usuario)
        {
            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@usuario", Usuario.ToString()));
            // Utiliza el método Leer de AccesoDA para obtener los datos del centro de operaciones
            DataTable tabla = ac.Leer("SP_Listar_Centros_Operaciones_Menu", parameters);
            List<BE.MenuAdmin> menus = new List<BE.MenuAdmin>();

            // Itera sobre cada fila del DataTable y crea un objeto menu para mostrar
            foreach (DataRow registro in tabla.Rows)
            {
                BE.MenuAdmin menu = new BE.MenuAdmin();
                menu.Id = int.Parse(registro["id"].ToString());
                menu.Controlador = registro["controlador"].ToString();
                menu.Nombre = registro["nombre"].ToString();
                menu.Ver = int.Parse(registro["ver"].ToString());

                menus.Add(menu);
            }
            return menus;
        }

        // Método para borrar una centro de operacion
        public int Borrar(int id)
        {
            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@id", id.ToString()));

            // Utiliza el método Escribir de AccesoDA para ejecutar el borrado y retorna el resultado
            return ac.Escribir("SP_Eliminar_Centro_Operaciones", parameters);
        }


        // Método para insertar un nuevo centro de operacion
        public int Insertar(BE.CentroOperaciones centroOperaciones)
        {

            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@nombre", centroOperaciones.Nombre));
            parameters.Add(ac.CrearParametro("@descripcion", centroOperaciones.Descripcion));
            parameters.Add(ac.CrearParametro("@direccion", centroOperaciones.Direccion));
            parameters.Add(ac.CrearParametro("@telefono", centroOperaciones.Telefono));
            parameters.Add(ac.CrearParametro("@correo", centroOperaciones.Correo));
            parameters.Add(ac.CrearParametro("@ruta_imagen", centroOperaciones.NombreImagen.ToString()));

            // Utiliza el método Escribir de AccesoDA para ejecutar la inserción y retorna el resultado
            return ac.Escribir("SP_Insertar_Centro_Operaciones", parameters);
        }

        // Método para editar un centro de operacion existente
        public int Editar(BE.CentroOperaciones centroOperaciones)
        {
            // Crea una lista de parámetros para enviar al método Escribir de AccesoDA
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@id", centroOperaciones.Codigo.ToString()));
            parameters.Add(ac.CrearParametro("@nombre", centroOperaciones.Nombre));
            parameters.Add(ac.CrearParametro("@descripcion", centroOperaciones.Descripcion));
            parameters.Add(ac.CrearParametro("@direccion", centroOperaciones.Direccion));
            parameters.Add(ac.CrearParametro("@telefono", centroOperaciones.Telefono));
            parameters.Add(ac.CrearParametro("@correo", centroOperaciones.Correo));
            parameters.Add(ac.CrearParametro("@ruta_imagen", centroOperaciones.NombreImagen.ToString()));

            // Utiliza el método Escribir de AccesoDA para ejecutar la actualización y retorna el resultado
            return ac.Escribir("SP_Editar_Centro_Operaciones", parameters);
        }
        

        // Método para obtener un centro de operacion específico por su ID
        public BE.CentroOperaciones Listar(int ID)
        {
            // Convierte el ID a string para crear el parámetro
            string valorecibido = Convert.ToString(ID);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@id", valorecibido));

            // Utiliza el método Leer de AccesoDA para obtener el centro de operacion específico
            DataTable tabla = ac.Leer("SP_Seleccionar_Centro_Operaciones", parameters);
            BE.CentroOperaciones centroOperaciones = new BE.CentroOperaciones();

            // Crea un objeto CentroOperaciones con los datos obtenidos del registro y lo retorna
            DataRow registro = tabla.Rows[0];
            centroOperaciones.Codigo = int.Parse(registro["id"].ToString());
            centroOperaciones.Nombre = registro["nombre"].ToString();
            centroOperaciones.Descripcion = registro["descripcion"].ToString();
            centroOperaciones.Direccion = registro["direccion"].ToString();
            centroOperaciones.Telefono = registro["telefono"].ToString();
            centroOperaciones.Correo = registro["correo"].ToString();
            centroOperaciones.NombreImagen = registro["ruta_imagen"].ToString();


            return centroOperaciones;
        }



        // Método para obtener un Centro de Operacion específico por su ID
        public BE.CentroOperaciones DetalleCentroOperacion(int id)
        {
            // Convierte el ID a string para crear el parámetro
            string valorecibido = Convert.ToString(id);
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(ac.CrearParametro("@id", valorecibido));

            // Utiliza el método Leer de AccesoDA para obtener el Centro de Operacion específico
            DataTable tabla = ac.Leer("SP_Seleccionar_Centro_Operacion", parameters);
            BE.CentroOperaciones centroOperaciones = new BE.CentroOperaciones();

            // Itera sobre cada fila del DataTable y crea un objeto centro de operacion para cada registro
            DataRow registro = tabla.Rows[0];
            BE.CentroOperaciones centroOperacion = new BE.CentroOperaciones();
                centroOperacion.Codigo = int.Parse(registro["id"].ToString()); 
                centroOperacion.Nombre = registro["nombre"].ToString();
                centroOperacion.Descripcion = registro["descripcion"].ToString();
                centroOperacion.Direccion = registro["direccion"].ToString();
                centroOperacion.Telefono = registro["telefono"].ToString();
                centroOperacion.Correo = registro["correo"].ToString();
            centroOperacion.Visitas = registro["visitas"].ToString();
            centroOperacion.NombreImagen = registro["ruta_imagen"].ToString();

            return centroOperacion;
        }

    }
}
