using SystemQuality.Models;

namespace SystemQuality.Services
{
    public class PrestamoService
    {
        private List<Prestamo> prestamos = new List<Prestamo>();

        // ── AGREGAR ──────────────────────────────────────────
        public void AgregarPrestamo(Prestamo prestamo)
        {
            prestamos.Add(prestamo);
            Console.WriteLine($"Préstamo de '{prestamo.Libro?.Titulo}' registrado.");
        }

        // ── ELIMINAR ─────────────────────────────────────────
        public bool EliminarPrestamo(Prestamo prestamo)
        {
            if (prestamos.Remove(prestamo))
            {
                Console.WriteLine("Préstamo eliminado.");
                return true;
            }
            Console.WriteLine("Préstamo no encontrado.");
            return false;
        }

        // ── OBTENER TODOS ─────────────────────────────────────
        public List<Prestamo> ObtenerTodos()
        {
            return prestamos;
        }

        // ── BÚSQUEDAS ─────────────────────────────────────────
        public List<Prestamo> BuscarPorEstado(EstadoPrestamo estado)
        {
            return prestamos.Where(p => p.Estado == estado).ToList();
        }

        public List<Prestamo> BuscarPorUsuario(string documento)
        {
            return prestamos.Where(p => p.Usuario?.Documento == documento).ToList();
        }

        // ── ORDENACIÓN ────────────────────────────────────────
        public List<Prestamo> OrdenarPorFechaLimite()
        {
            return prestamos.OrderBy(p => p.FechaLimite).ToList();
        }

        // ── KPIs / ESTADÍSTICAS ───────────────────────────────
        public int TotalPrestamos()
        {
            return prestamos.Count;
        }

        public int PrestamosActivos()
        {
            return prestamos.Count(p => p.Estado == EstadoPrestamo.Activo);
        }

        public int PrestamosVencidos()
        {
            // Actualiza el estado si ya venció
            foreach (var p in prestamos)
                if (p.EstaVencido()) p.Estado = EstadoPrestamo.Vencido;

            return prestamos.Count(p => p.Estado == EstadoPrestamo.Vencido);
        }

        public int PrestamosDevueltos()
        {
            return prestamos.Count(p => p.Estado == EstadoPrestamo.Devuelto);
        }

        public double PromedioDiasPrestamo()
        {
            if (prestamos.Count == 0) return 0;
            return prestamos.Average(p => p.DiasTranscurridos());
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n ESTADÍSTICAS DE PRÉSTAMOS");
            Console.WriteLine($"   Total de préstamos : {TotalPrestamos()}");
            Console.WriteLine($"   Activos            : {PrestamosActivos()}");
            Console.WriteLine($"   Vencidos           : {PrestamosVencidos()}");
            Console.WriteLine($"   Devueltos          : {PrestamosDevueltos()}");
            Console.WriteLine($"   Promedio días      : {PromedioDiasPrestamo():F1} días");
        }
    }
}