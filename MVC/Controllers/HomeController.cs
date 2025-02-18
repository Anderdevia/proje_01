using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public partial class HomeController : Controller
    {
        CentroOperacionesController gestor = new CentroOperacionesController();
        Proyecto.BL.CentroOperaciones gestorCentroOperacion = new Proyecto.BL.CentroOperaciones();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // Método para mostrar la lista de los menus 
        public ActionResult ListarMenu()
        {
            var menus = gestorCentroOperacion.ListarMenus();
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


        // Método para mostrar la lista de los centros de operaciones creados 
        public ActionResult ListarCentroOperaciones()
        {
            var Usuario = Session["Usuario"];
            var menus = gestorCentroOperacion.ListarCentroOperaciones(Usuario.ToString());
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
        public ActionResult verificaAccesoCentro(int id_centro_operacion, int opcion, int id_usuario)
        {

            var visitas = gestorCentroOperacion.verificaAccesoCentro(id_usuario, id_centro_operacion, opcion);
            List<Visitas> data = new List<Visitas>();

            foreach (var dr in visitas)
            {
                Visitas obj = new Visitas()
                {
                    Nombre = dr.Nombre.ToString()
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);


        }



    }
}