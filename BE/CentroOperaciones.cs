using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BE
{
    public class CentroOperaciones
    {

        private int codigo;

        [Required]
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }


        private string nombre;

        [Display(Name = "Nombre")]
        [Required]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string descripcion;

        [Display(Name = "Descripción")]
        [Required]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string direccion;

        [Display(Name = "Dirección")]
        [Required]
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private string telefono;

        [Display(Name = "Telefono")]
        [Required]
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private string correo;

        [Display(Name = "Correo")]
        [Required]
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        private string visitas;

        [Display(Name = "Visitas")]
        [Required]
        public string Visitas
        {
            get { return visitas; }
            set { visitas = value; }
        }

        private HttpPostedFileBase ruta_imagen;


        [Display(Name = "Ruta Imagen")]
        public HttpPostedFileBase Ruta_imagen
        {
            get { return ruta_imagen; }
            set { ruta_imagen = value; }
        }

        private string nombreImagen;

        public string NombreImagen
        {
            get { return nombreImagen; }
            set { nombreImagen = value; }
        }
    }
}
