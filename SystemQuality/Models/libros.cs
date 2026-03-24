using System;

namespace SystemQuality.Models
{
    public class Libro
    {
        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public string ISBN { get; set; } = "";
        public bool Disponible { get; set; } = true;

        public Libro()
        {
            Disponible = true;
        }

        public Libro(string titulo, string autor, string isbn)
        {
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            Disponible = true;
        }

            public string ResumenCorto()
{
    return $"{Titulo} - {Autor}";
}

public string DetalleCompleto()
{
    return $"Título: {Titulo}\nAutor: {Autor}\nISBN: {ISBN}\nDisponible: {Disponible}";
}

public override string ToString()
{
    return DetalleCompleto();

        }
    }
}


