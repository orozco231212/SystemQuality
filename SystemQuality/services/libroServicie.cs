using SystemQuality.Models;

namespace SystemQuality.Services
{
    public class LibroService
    {
        private List<Libro> libros = new List<Libro>();

        // ── AGREGAR ──────────────────────────────────────────
        public void AgregarLibro(Libro libro)
        {
            libros.Add(libro);
            Console.WriteLine($" Libro '{libro.Titulo}' agregado.");
        }

        // ── ELIMINAR ─────────────────────────────────────────
        public bool EliminarLibro(string isbn)
        {
            Libro? libro = libros.FirstOrDefault(l => l.ISBN == isbn);
            if (libro != null)
            {
                libros.Remove(libro);
                Console.WriteLine($"Libro con ISBN '{isbn}' eliminado.");
                return true;
            }
            Console.WriteLine($" No se encontró el libro con ISBN '{isbn}'.");
            return false;
        }

        // ── OBTENER TODOS ─────────────────────────────────────
        public List<Libro> ObtenerTodos()
        {
            return libros;
        }

        // ── BÚSQUEDAS ─────────────────────────────────────────
        public Libro? BuscarPorISBN(string isbn)
        {
            return libros.FirstOrDefault(l => l.ISBN == isbn);
        }

        public List<Libro> BuscarPorTitulo(string titulo)
        {
            return libros.Where(l => l.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Libro> BuscarPorAutor(string autor)
        {
            return libros.Where(l => l.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // ── ORDENACIÓN ────────────────────────────────────────
        public List<Libro> OrdenarPorTitulo()
        {
            return libros.OrderBy(l => l.Titulo).ToList();
        }

        // ── KPIs / ESTADÍSTICAS ───────────────────────────────
        public int TotalLibros()
        {
            return libros.Count;
        }

        public int LibrosDisponibles()
        {
            return libros.Count(l => l.Disponible);
        }

        public int LibrosPrestados()
        {
            return libros.Count(l => !l.Disponible);
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n ESTADÍSTICAS DE LIBROS ");
            Console.WriteLine($"   Total de libros   : {TotalLibros()}");
            Console.WriteLine($"   Disponibles       : {LibrosDisponibles()}");
            Console.WriteLine($"   Prestados         : {LibrosPrestados()}");
        }

        // ── COMPARACIÓN ARRAY vs LIST ─────────────────────────
        public void CompararArrayVsLista()
        {
            Console.WriteLine("\nARRAY vs LIST");

            // Con Array: tamaño fijo, no se puede agregar sin crear uno nuevo
            string[] arrayLibros = new string[3];
            arrayLibros[0] = "Cien años de soledad";
            arrayLibros[1] = "El principito";
            arrayLibros[2] = "Don Quijote";
            Console.WriteLine("Array (tamaño fijo = 3): " + string.Join(", ", arrayLibros));

            // Con List: tamaño dinámico, se puede agregar y eliminar libremente
            List<string> listaLibros = new List<string>();
            listaLibros.Add("Cien años de soledad");
            listaLibros.Add("El principito");
            listaLibros.Add("Don Quijote");
            listaLibros.Add("Harry Potter"); 
            Console.WriteLine("List (tamaño dinámico): " + string.Join(", ", listaLibros));

            Console.WriteLine(" La List es más flexible: crece/decrece sin redeclarar.");
        }
    }
}