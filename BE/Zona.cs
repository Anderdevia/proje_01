using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Zona
    {
        private int id;

        [DisplayName("Id")]
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
    }
}
