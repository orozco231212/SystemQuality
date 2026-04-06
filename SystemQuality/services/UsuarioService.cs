using SystemQuality.Models;

namespace SystemQuality.Services
{
    public class UsuarioService
    {
        private List<Usuario> usuarios = new List<Usuario>();

        // ── AGREGAR ──────────────────────────────────────────
        public void AgregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
            Console.WriteLine($"Usuario '{usuario.Nombre}' agregado.");
        }

        // ── ELIMINAR ─────────────────────────────────────────
        public bool EliminarUsuario(string documento)
        {
            Usuario? usuario = usuarios.FirstOrDefault(u => u.Documento == documento);
            if (usuario != null)
            {
                usuarios.Remove(usuario);
                Console.WriteLine($"Usuario con documento '{documento}' eliminado.");
                return true;
            }
            Console.WriteLine($"No se encontró el usuario con documento '{documento}'.");
            return false;
        }

        // ── OBTENER TODOS ─────────────────────────────────────
        public List<Usuario> ObtenerTodos()
        {
            return usuarios;
        }

        // ── BÚSQUEDAS ─────────────────────────────────────────
        public Usuario? BuscarPorDocumento(string documento)
        {
            return usuarios.FirstOrDefault(u => u.Documento == documento);
        }

        public List<Usuario> BuscarPorNombre(string nombre)
        {
            return usuarios.Where(u => u.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // ── ORDENACIÓN ────────────────────────────────────────
        public List<Usuario> OrdenarPorNombre()
        {
            return usuarios.OrderBy(u => u.Nombre).ToList();
        }

        // ── KPIs / ESTADÍSTICAS ───────────────────────────────
        public int TotalUsuarios()
        {
            return usuarios.Count;
        }

        public int UsuariosActivos()
        {
            return usuarios.Count(u => u.Activo);
        }

        public int UsuariosInactivos()
        {
            return usuarios.Count(u => !u.Activo);
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n ESTADÍSTICAS DE USUARIOS ");
            Console.WriteLine($"   Total de usuarios : {TotalUsuarios()}");
            Console.WriteLine($"   Activos           : {UsuariosActivos()}");
            Console.WriteLine($"   Inactivos         : {UsuariosInactivos()}");
        }
    }
}