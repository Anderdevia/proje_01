using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Clientes
    {
        private int id;

        [DisplayName("Id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string nombre;

        [DisplayName("Nombre")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string zona;

        [DisplayName("Zona")]
        public string Zona
        {
            get { return zona; }
            set { zona = value; }
        }

        private string usuarioLogin;

        [DisplayName("Usuario")]
        public string UsuarioLogin
        {
            get { return usuarioLogin; }
            set { usuarioLogin = value; }
        }

        private string correo;

        [DisplayName("Correo")]
        public string Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        private string contraseña;

        [DisplayName("Contraseña")]
        public string Contraseña
        {
            get { return contraseña; }
            set { contraseña = value; }
        }

        private int habeasData;

        [DisplayName("Habeas Data")]
        public int HabeasData
        {
            get { return habeasData; }
            set { habeasData = value; }
        }

        [DisplayName("Rol")]
        public Rol IdRol
        {get; set;}
    }
}
