using BE;
using IdentityServer3.Core.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IdentityServer3.Core.Events.EventConstants;

namespace MVC.Controllers
{
    [Authorize]
    public class CentroOperacionesController : Controller
    {

        // Instancia del gestor de centro de operaciones desde la capa de negocio
        Proyecto.BL.CentroOperaciones gestor = new Proyecto.BL.CentroOperaciones();

        // GET: Centro de Operaciones
        public ActionResult CentroOperaciones()
        {
            return View();
        }

        public ActionResult DetalleCentroOperacion(int id)
        {

            BE.CentroOperaciones centroOperacion = gestor.DetalleCentroOperacion(id);

            centroOperacion.NombreImagen = centroOperacion.NombreImagen.Replace("~", "");

            return View(centroOperacion);

        }

        // GET: Buscar por fecha clientes
        public ActionResult FiltroFecha()
        {
            return View();
        }

        // Método para mostrar la lista de los menus 
        public ActionResult ListarMenu()
        {
            var menus = gestor.ListarMenus();
            List<MenuAdmin> data = new List<MenuAdmin>();

            foreach (var dr in menus)
            {
                MenuAdmin obj = new MenuAdmin()
                {
                    Id = int.Parse(dr.Id.ToString()),
                    Controlador = dr.Controlador.ToString(),
                    Nombre = dr.Nombre.ToString(),
                    Ver = int.Parse(dr.Ver.ToString())
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        // Método para mostrar la lista de los centro de operaciones  
        public ActionResult ListarCentroOperaciones()
        {
            var Usuario = Session["Usuario"];
            var menus = gestor.ListarCentroOperaciones(Usuario.ToString());
            List<MenuAdmin> data = new List<MenuAdmin>();

            foreach (var dr in menus)
            {
                MenuAdmin obj = new MenuAdmin()
                {
                    Id = int.Parse(dr.Id.ToString()),
                    Controlador = dr.Controlador.ToString(),
                    Nombre = dr.Nombre.ToString(),
                    Ver = int.Parse(dr.Ver.ToString())
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        // Método para mostrar el usuario si  ya entro  a visitar el centro de operacion   
        public ActionResult verificaAccesoCentro(int id_centro_operacion,int opcion, int id_usuario)
        {


            var menus = gestor.verificaAccesoCentro(id_usuario, id_centro_operacion, opcion);
            List<Visitas> data = new List<Visitas>();

            foreach (var dr in menus)
            {
                Visitas obj = new Visitas()
                {
                    Nombre = dr.Nombre.ToString()
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        // Método para mostrar la listas de los clientes 
        public ActionResult ListaClientes(string fechaDesde, string fechaHasta, int opcion)
        {
            var centroOperaciones = gestor.ListaClientes(fechaDesde, fechaHasta, opcion);
            List<Visitas> data = new List<Visitas>();

            foreach (var dr in centroOperaciones)
            {
                Visitas obj = new Visitas()
                {
                    Id = int.Parse(dr.Id.ToString()),
                    Nombre = dr.Nombre.ToString(),
                    Id_centro_operacion = dr.Id_centro_operacion.ToString(),
                    Visitas_aceptadas = dr.Visitas_aceptadas.ToString(),
                    Fecha = dr.Fecha.ToString()
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        // Método para mostrar la listas de los centros de operaciones
        public ActionResult ListaCentroOperacion()
        {
            var centroOperaciones = gestor.Listar();
            List<CentroOperaciones> data = new List<CentroOperaciones>();

            foreach (var dr in centroOperaciones)
            {
                CentroOperaciones obj = new CentroOperaciones()
                {
                    Codigo = int.Parse(dr.Codigo.ToString()),
                    Nombre = dr.Nombre.ToString(),
                    Direccion = dr.Direccion.ToString(),
                    Telefono=dr.Telefono.ToString(),
                    Correo=dr.Correo.ToString(),
                    Descripcion = dr.Descripcion.ToString()
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        // Método para guardar la imagen del centro de operacion
        public ActionResult imagenCentroOperacion(CentroOperaciones centroOperaciones, HttpPostedFileBase Imagen)
        {
            // se agrega la ruta local para poder grabr las imagenes dentro del sistema y asi se puedan recuperar 
            Imagen.SaveAs(Path.Combine(Server.MapPath("~/img/"),
            Path.GetFileName(Imagen.FileName)));
            return Content("1");

        }

        public ActionResult nuevoCentroOperacion(CentroOperaciones centroOperaciones, HttpPostedFileBase Imagen)
        {
            //se agrega el modelo para realizar el registro del centro de operacion nuevo
            gestor.Grabar(centroOperaciones);


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        // Método para mostrar la vista de edición de centro de operacion basado en un ID
        public ActionResult Update(int id)
        {
            // Llama al método Listar(id) del gestor para obtener el centro de operacion específico
            var centroOperaciones = gestor.Listar(id);

            return Json(centroOperaciones, JsonRequestBehavior.AllowGet);
        }


        // Método para mostrar la vista de confirmación de eliminación basado en un ID
        public ActionResult Delete(int id)
        {
            // Llama al método Borrar(id) del gestor para eliminar el centro de operacion específico
            var idRespuesta = gestor.Borrar(id);

            var centroOperaciones = gestor.Listar();
            List<CentroOperaciones> data = new List<CentroOperaciones>();

            foreach (var dr in centroOperaciones)
            {
                CentroOperaciones obj = new CentroOperaciones()
                {
                    Codigo = int.Parse(dr.Codigo.ToString()), 
                    Nombre = dr.Nombre.ToString(),     
                    Descripcion = dr.Descripcion.ToString(),
                    NombreImagen = dr.NombreImagen.ToString()
                };
                data.Add(obj);
            }
            // Retorna la vista de confirmación de eliminación 
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //Actualiza los campos de la centro de operacion
        public ActionResult actualizaCentroOperacion(CentroOperaciones centroOperaciones, string imagen)
        {
            if (centroOperaciones.NombreImagen != imagen)
            {
                string rutaDirectorio = Path.Combine(Server.MapPath("" + imagen));


                if (System.IO.File.Exists(rutaDirectorio))
                {
                    System.IO.File.Delete(rutaDirectorio);
                }
                else
                {
                }
            }

            //se agrega el modelo para realizar el registro del centro de operacion nuevo
            gestor.Grabar(centroOperaciones);


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

    }
}