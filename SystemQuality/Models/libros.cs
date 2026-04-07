using System;

namespace SystemQuality.Models
{
    public class Libro
    {
        // ── ID AUTOMÁTICO ─────────────────────────────────────
        private static int _contador = 0;
        public string Id { get; set; } = "";

        public string Titulo { get; set; } = "";
        public string Autor { get; set; } = "";
        public string ISBN { get; set; } = "";
        public bool Disponible { get; set; } = true;

        public Libro()
        {
            _contador++;
            Id = $"LIB-{_contador:D3}";
            Disponible = true;
        }

        public Libro(string titulo, string autor, string isbn)
        {
            _contador++;
            Id = $"LIB-{_contador:D3}";
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            Disponible = true;
        }

        public string ResumenCorto()
        {
            return $"[{Id}] {Titulo} - {Autor}";
        }

        public string DetalleCompleto()
        {
            return $"ID        : {Id}\nTítulo    : {Titulo}\nAutor     : {Autor}\nISBN      : {ISBN}\nDisponible: {(Disponible ? "Sí" : "No")}";
        }

        public override string ToString()
        {
            return DetalleCompleto();
        }
    }
}