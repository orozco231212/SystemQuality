using System;

namespace SystemQuality.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }

        public Usuario()
        {
            Activo = true;
        }

        public Usuario(string nombre, string documento, string email)
        {
            Nombre = nombre;
            Documento = documento;
            Email = email;
            Activo = true;

        }
    }
}