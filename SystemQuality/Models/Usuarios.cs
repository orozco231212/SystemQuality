using System;

namespace SystemQuality.Models
{
    public class Usuario
    {
        public string Nombre { get; set; } = "";  
        public string Documento { get; set; } = "";
        public string Email { get; set; } = "";
        public bool Activo { get; set; } = true;

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

        public string ResumenCorto()
{
    return $"{Nombre} - {Documento}";
}

public string DetalleCompleto()
{
    return $"Nombre: {Nombre}\nDocumento: {Documento}\nEmail: {Email}\nActivo: {Activo}";
}

public override string ToString()
{
    return DetalleCompleto();



        }
    }
}


