using System;

namespace SystemQuality.Models
{
    public class Prestamo
    {
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaLimite { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public EstadoPrestamo Estado { get; set; }

        public Prestamo()
        {
            Estado = EstadoPrestamo.Activo;
            FechaDevolucion = null;
        }

        public Prestamo(Libro libro, Usuario usuario, DateTime fechaPrestamo, DateTime fechaLimite)
        {
            Libro = libro;
            Usuario = usuario;
            FechaPrestamo = fechaPrestamo;
            FechaLimite = fechaLimite;
            Estado = EstadoPrestamo.Activo;
            FechaDevolucion = null;

            
        }
    }
}