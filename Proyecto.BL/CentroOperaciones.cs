using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BL
{
    public class CentroOperaciones
    {
        // Instancia del acceso a datos para el centro de operaciones desde la capa de datos
        Proyecto.DA.MP_CentroOperaciones MP = new DA.MP_CentroOperaciones();


        // Método para guardar o actualizar un centro de operacion
        public int Grabar(BE.CentroOperaciones centroOperacion)
        {
            int res = 0;
            // Verifica si el ID de la centro de operacion es 0 (nuevo centro de operacion)
            if (centroOperacion.Codigo == 0)
            {
                // Llama al método Insertar() de la capa de datos para agregar la nueva centro de operacion
                res = MP.Insertar(centroOperacion);
            }
            else
            {
                // Llama al método Editar() de la capa de datos para actualizar la centro de operacion existente
                res = MP.Editar(centroOperacion);
            }
            // Retorna el resultado de la operación (número de filas afectadas)
            return res;
        }

        // metodo para listar los cetro de operaciones
        public List<BE.CentroOperaciones> Listar()
        {
            // Llama al método listar() de la capa de datos para recuperar la lista de los centro de operaciones
            return MP.listar();
        }

        // Metodo para listar los clientes
        public List<BE.Visitas> ListaClientes(string fechaDesde, string fechaHasta, int opcion)
        {
            // Llama al método listar() de la capa de datos para recuperar la lista de clientes
            return MP.ListaClientes(fechaDesde, fechaHasta, opcion);
        }


        public List<BE.MenuAdmin> ListarMenus()
        {
            // Llama al método listar() de la capa de datos para recuperar la lista de menus
            return MP.listarMenu();
        }

        //Metodo para mostrar los centros de operaciones
        public List<BE.MenuAdmin> ListarCentroOperaciones(string Usuario)
        {
            // Llama al método listar() de la capa de datos para recuperar la lista de los centro de operaciones
            return MP.ListarCentroOperaciones(Usuario);
        }

        // Metodo para verificar el acceso al centro de operaciones
        public List<BE.Visitas> verificaAccesoCentro(int id_usuario, int id_centro_operacion, int opcion)
        {
            // Llama al método listar() de la capa de datos para recuperar la lista de los centros de operaciones
            return MP.verificaAccesoCentro(id_usuario, id_centro_operacion, opcion);
        }

        // Método para obtener una centro de operacion en específico basado en su ID
        public BE.CentroOperaciones Listar(int id)
        {
            // Llama al método Listar(id) de la capa de datos para recuperar un centro de operacion específico
            return MP.Listar(id);
        }

        //Metodo para mostrar el detalle del centro de operacion
        public BE.CentroOperaciones DetalleCentroOperacion(int id)
        {
            // Llama al método Listar(id) de la capa de datos para recuperar un centro de operacion específico
            return MP.DetalleCentroOperacion(id);
        }

        // Método para eliminar el centro de operación
        public int Borrar(int id)
        {
            // Llama al método Borrar() de la capa de datos para eliminar el centro de operación
            return MP.Borrar(id);
        }

    }
}
