using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Visitas
    {
        private int id;

        [Required]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        private string nombre;
     
        [Display(Name = "Nombre")]
        [Required]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string id_centro_operacion;

        [Display(Name = "Id Centro Operación")]
        [Required]
        public string Id_centro_operacion
        {
            get { return id_centro_operacion; }
            set { id_centro_operacion = value; }
        }

        private int id_usuario;

        [Display(Name = "Id Usuario")]
        [Required]
        public int Id_usuario
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        private string visitas_aceptadas;

        [Display(Name = "Visitas Aceptadas")]
        [Required]
        public string Visitas_aceptadas
        {
            get { return visitas_aceptadas; }
            set { visitas_aceptadas = value; }
        }

        private string visitas_rechazadas;

        [Display(Name = "Visitas Rechazadas")]
        [Required]
        public string Visitas_rechazadas
        {
            get { return visitas_rechazadas; }
            set { visitas_rechazadas = value; }
        }

        private string fecha;

        [Display(Name = "Fecha")]
        [Required]
        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
}
