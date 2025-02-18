using BE;
using Proyecto.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        // Instancia del gestor de Login desde la capa de negocio
        Proyecto.BL.Login gestor = new Proyecto.BL.Login();
        Proyecto.BL.Zona gestorZona = new Proyecto.BL.Zona();

        // GET: Login
        public ActionResult AccederLogin()
        {
            ViewBag.Nombre_Login = Constantes.Sources._NOMBRE_LOGIN;
            ViewBag.Message = "";
            return View();
        }

        // Método para acceder despues de logearse
        [HttpPost]
        public ActionResult AccederLogin(string correo, string contraseña)
        {
            ViewBag.Nombre_Login = Constantes.Sources._NOMBRE_LOGIN;
            // Llama al método AccederLogin(id) del gestor 
            if (correo != null)
            {
                var clientes = gestor.AccederLogin(correo, contraseña);



                if (clientes.UsuarioLogin == null)
                {
                    ViewBag.Message = "Acceso denegado, credenciales incorrectas. ";
                    return View();
               
                }
                else if (!string.IsNullOrEmpty(clientes.UsuarioLogin))
                {

                    FormsAuthentication.SetAuthCookie(clientes.UsuarioLogin, false);
                    Session["Usuario"] = clientes.UsuarioLogin;
                    TempData["UsuarioId"] = clientes.Id;
                    ViewBag.Usuario = clientes.UsuarioLogin; 
                    return RedirectToAction("CentroOperaciones", "CentroOperaciones"); 

                }
                else
                {

                    ViewBag.Message = "";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "";
                return View();
            }
        }

        [Authorize]
        public ActionResult CerrarSesion()
        {

            FormsAuthentication.SignOut();
            Session["Usuario"] = null;

            ViewBag.Message = "";
            return RedirectToAction("AccederLogin", "Login");



        }

        // Metodo para ver el combo de la zonas de la ciudad
        [HttpGet]
        public JsonResult GetComboZona()
        {
            var Zonas = gestorZona.Listar();
            List<BE.Zona> data = new List<BE.Zona>();

            foreach (var dr in Zonas)
            {
                BE.Zona obj = new BE.Zona()
                {
                    Id = int.Parse(dr.Id.ToString()), 
                    Nombre = dr.Nombre.ToString()   
                };
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        // Metodo para registrar el nuevo cliente
        public ActionResult NuevoCliente(Clientes cliente)
        {

            var clientes = gestor.VerificarAccederLogin(cliente.UsuarioLogin, cliente.Contraseña);



            if (clientes.UsuarioLogin == null)
            {
                gestor.Grabar(cliente);


                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else if (!string.IsNullOrEmpty(clientes.UsuarioLogin))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);


        }
    }
}