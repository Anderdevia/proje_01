using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class MenuAdmin
    {
        private int id;

        [DisplayName("Id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string controlador;

        [DisplayName("Controllador")]
        public string Controlador
        {
            get { return controlador; }
            set { controlador = value; }
        }

        private string nombre;

        [DisplayName("Nombre")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private int ver;

        [DisplayName("Ver")]
        public int Ver
        {
            get { return ver; }
            set { ver = value; }
        }

        
    }
}
