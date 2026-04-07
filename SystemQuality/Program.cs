using System;
using SystemQuality.Models;
using SystemQuality.Services;

class Program
{
    //===============================
    //MENU PRINCIPAL DE LA BIBLIOTECA
    //===============================

    static LibroService    libroService    = new LibroService();
    static UsuarioService  usuarioService  = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();

    static void Main() { ShowMainMenu(); }

    //===================
    //MENU  DE NAVEGACION
    //===================

    static void ShowMainMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("SISTEMA BIBLIOTECA");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Prestamos");
            Console.WriteLine("4. Busquedas y Reportes");
            Console.WriteLine("5. Guardar / Cargar Datos");
            Console.WriteLine("6. Salir");
            Console.WriteLine("7. Probar modelos");
            Console.WriteLine("8. Probar Servicios");

            Console.Write("Seleccione una opcion: ");

            if (!int.TryParse(Console.ReadLine(), out option))
            {
                option = 0;
            }

            switch (option)
            {
                case 1: ShowBooksMenu(); break;
                case 2: ShowUsersMenu(); break;
                case 3: ShowLoansMenu(); break;
                case 4: ShowSearchReportsMenu(); break;
                case 5: ShowPersistenceMenu(); break;
                case 6: ConfirmExitAndSave(); break;
                case 7: MenuPruebas(); break;
                case 8: MenuServicios(); break;
                default:
                    Console.WriteLine("Opcion invalida");
                    Pause();
                    break;
            }

        } while (option != 6);
    }

    //===================
    // MENU DE PRUEBAS
    //===================
    static void MenuPruebas()
    {
        Console.Clear();
        Console.WriteLine("=== PRUEBA DE MODELOS ===");

        Libro libro = new Libro
        {
            Titulo = "Cien años de soledad",
            Autor = "Gabriel García Márquez",
            ISBN = "123456"
        };

        Console.WriteLine("\nLibro creado:");
        Console.WriteLine($"ID    : {libro.Id}");
        Console.WriteLine($"Titulo: {libro.Titulo}");
        Console.WriteLine($"Autor : {libro.Autor}");
        Console.WriteLine($"ISBN  : {libro.ISBN}");

        Usuario usuario = new Usuario
        {
            Nombre = "Juan Perez",
            Documento = "123456789",
            Email = "juan@email.com"
        };

        Console.WriteLine("\nUsuario creado:");
        Console.WriteLine($"Nombre   : {usuario.Nombre}");
        Console.WriteLine($"Documento: {usuario.Documento}");
        Console.WriteLine($"Email    : {usuario.Email}");

        Prestamo prestamo = new Prestamo
        {
            Libro = libro,
            Usuario = usuario
        };
        Console.WriteLine("\nPrestamo creado:");
        Console.WriteLine($"Libro  : {prestamo.Libro.Titulo}");
        Console.WriteLine($"Usuario: {prestamo.Usuario.Nombre}");

        Pause();
    }

    // =========================
    // MENU PRUEBA DE SERVICES
    // =========================
    static void MenuServicios()
    {
        Console.Clear();
        Console.WriteLine("=== PRUEBA DE SERVICES ===\n");

        // -- Libros registrados --
        Console.WriteLine("-- Libros registrados --");
        var libros = libroService.ObtenerTodos();
        if (libros.Count == 0)
            Console.WriteLine("  No hay libros registrados.");
        else
            foreach (var l in libros)
                Console.WriteLine($"  [{l.Id}] [{(l.Disponible ? "DISPONIBLE" : "PRESTADO")}] {l.Titulo} - {l.Autor} | ISBN: {l.ISBN}");

        // -- Usuarios registrados --
        Console.WriteLine("\n-- Usuarios registrados --");
        var usuarios = usuarioService.ObtenerTodos();
        if (usuarios.Count == 0)
            Console.WriteLine("  No hay usuarios registrados.");
        else
            foreach (var u in usuarios)
                Console.WriteLine($"  [{(u.Activo ? "ACTIVO" : "INACTIVO")}] {u.Nombre} | Doc: {u.Documento}");

        // -- Préstamos registrados --
        Console.WriteLine("\n-- Préstamos registrados --");
        var prestamos = prestamoService.ObtenerTodos();
        if (prestamos.Count == 0)
            Console.WriteLine("  No hay préstamos registrados.");
        else
            foreach (var p in prestamos)
                Console.WriteLine($"  [{p.Estado}] {p.Libro?.Titulo} → {p.Usuario?.Nombre} | Vence: {p.FechaLimite:dd/MM/yyyy}");

        // -- KPIs --
        libroService.MostrarEstadisticas();
        usuarioService.MostrarEstadisticas();
        prestamoService.MostrarEstadisticas();

        // -- Array vs List --
        libroService.CompararArrayVsLista();

        Pause();
    }

    // ==========
    // MENÚ LIBROS
    // ===========

    static void ShowBooksMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine(" MENU LIBROS ");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Ver detalle");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("0. Volver");

            option = int.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 1: RegisterBook(); break;
                case 2: ListBooksMenu(); break;
                case 3: ViewBookDetail(); break;
                case 4: UpdateBookMenu(); break;
                case 5: DeleteBook(); break;
            }

        } while (option != 0);
    }

    static void RegisterBook()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LIBRO ===\n");
        Console.Write("Titulo : "); string titulo = Console.ReadLine()!;
        Console.Write("Autor  : "); string autor  = Console.ReadLine()!;
        Console.Write("ISBN   : "); string isbn   = Console.ReadLine()!;
        Libro libro = new Libro(titulo, autor, isbn);
        libroService.AgregarLibro(libro);
        Console.WriteLine($"ID asignado automáticamente: {libro.Id}");
        Pause();
    }

    static void ListBooksMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("LISTAR LIBROS");
            Console.WriteLine("1. Todos");
            Console.WriteLine("2. Disponibles");
            Console.WriteLine("3. Prestados");
            Console.WriteLine("0. Volver");

            option = int.Parse(Console.ReadLine()!);
            switch (option)
            {
                case 1: ListBooksAll(); break;
                case 2: ListBooksAvailable(); break;
                case 3: ListBooksBorrowed(); break;
            }

        } while (option != 0);
    }

    static void ListBooksAll()
    {
        Console.Clear();
        Console.WriteLine("=== TODOS LOS LIBROS ===\n");
        var lista = libroService.ObtenerTodos();
        if (lista.Count == 0)
            Console.WriteLine("No hay libros registrados.");
        else
            foreach (var l in lista)
                Console.WriteLine($"  [{l.Id}] [{(l.Disponible ? "DISPONIBLE" : "PRESTADO")}] {l.Titulo} - {l.Autor} | ISBN: {l.ISBN}");
        Pause();
    }

    static void ListBooksAvailable()
    {
        Console.Clear();
        Console.WriteLine("=== LIBROS DISPONIBLES ===\n");
        var lista = libroService.ObtenerTodos().Where(l => l.Disponible).ToList();
        if (lista.Count == 0)
            Console.WriteLine("No hay libros disponibles.");
        else
            foreach (var l in lista)
                Console.WriteLine($"  [{l.Id}] {l.Titulo} - {l.Autor} | ISBN: {l.ISBN}");
        Pause();
    }

    static void ListBooksBorrowed()
    {
        Console.Clear();
        Console.WriteLine("=== LIBROS PRESTADOS ===\n");
        var lista = libroService.ObtenerTodos().Where(l => !l.Disponible).ToList();
        if (lista.Count == 0)
            Console.WriteLine("No hay libros prestados.");
        else
            foreach (var l in lista)
                Console.WriteLine($"  [{l.Id}] {l.Titulo} - {l.Autor} | ISBN: {l.ISBN}");
        Pause();
    }

    static void ViewBookDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE LIBRO ===\n");
        Console.Write("Ingrese ISBN: ");
        string isbn = Console.ReadLine()!;
        var libro = libroService.BuscarPorISBN(isbn);
        if (libro == null) Console.WriteLine("Libro no encontrado.");
        else Console.WriteLine(libro.DetalleCompleto());
        Pause();
    }

    static void UpdateBookMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine(" ACTUALIZAR LIBRO ");
            Console.WriteLine("1. Editar titulo");
            Console.WriteLine("2. Editar autor");
            Console.WriteLine("3. Editar ISBN");
            Console.WriteLine("0. Volver");

            option = int.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 1: EditBookTitle(); break;
                case 2: EditBookAuthor(); break;
                case 3: EditBookYearCategory(); break;
            }

        } while (option != 0);
    }

    static void EditBookTitle()
    {
        Console.Clear();
        Console.WriteLine("=== EDITAR TÍTULO ===\n");
        Console.Write("ISBN del libro: ");
        string isbn = Console.ReadLine()!;
        var libro = libroService.BuscarPorISBN(isbn);
        if (libro == null) { Console.WriteLine("Libro no encontrado."); Pause(); return; }
        Console.Write("Nuevo título: ");
        libro.Titulo = Console.ReadLine()!;
        Console.WriteLine("Título actualizado correctamente.");
        Pause();
    }

    static void EditBookAuthor()
    {
        Console.Clear();
        Console.WriteLine("=== EDITAR AUTOR ===\n");
        Console.Write("ISBN del libro: ");
        string isbn = Console.ReadLine()!;
        var libro = libroService.BuscarPorISBN(isbn);
        if (libro == null) { Console.WriteLine("Libro no encontrado."); Pause(); return; }
        Console.Write("Nuevo autor: ");
        libro.Autor = Console.ReadLine()!;
        Console.WriteLine("Autor actualizado correctamente.");
        Pause();
    }

    static void EditBookYearCategory()
    {
        Console.Clear();
        Console.WriteLine("=== EDITAR ISBN ===\n");
        Console.Write("ISBN actual del libro: ");
        string isbn = Console.ReadLine()!;
        var libro = libroService.BuscarPorISBN(isbn);
        if (libro == null) { Console.WriteLine("Libro no encontrado."); Pause(); return; }
        Console.Write("Nuevo ISBN: ");
        libro.ISBN = Console.ReadLine()!;
        Console.WriteLine("ISBN actualizado correctamente.");
        Pause();
    }

    static void DeleteBook()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR LIBRO ===\n");
        Console.Write("Ingrese ISBN del libro a eliminar: ");
        string isbn = Console.ReadLine()!;
        libroService.EliminarLibro(isbn);
        Pause();
    }

    // =========================
    // USUARIOS
    // =========================

    static void ShowUsersMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("MENU USUARIOS");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Ver detalle");
            Console.WriteLine("4. Actualizar usuario");
            Console.WriteLine("5. Eliminar usuario");
            Console.WriteLine("0. Volver");

            Console.Write("Seleccione una opción: ");
            if (!int.TryParse(Console.ReadLine(), out option)) option = 0;

            switch (option)
            {
                case 1: RegisterUser(); break;
                case 2: ListUsers(); break;
                case 3: ViewUserDetail(); break;
                case 4: UpdateUserMenu(); break;
                case 5: DeleteUser(); break;
            }

        } while (option != 0);
    }

    static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR USUARIO ===\n");
        Console.Write("Nombre    : "); string nombre    = Console.ReadLine()!;
        Console.Write("Documento : "); string documento = Console.ReadLine()!;
        Console.Write("Email     : "); string email     = Console.ReadLine()!;
        usuarioService.AgregarUsuario(new Usuario(nombre, documento, email));
        Pause();
    }

    static void ListUsers()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE USUARIOS ===\n");
        var lista = usuarioService.ObtenerTodos();
        if (lista.Count == 0)
            Console.WriteLine("No hay usuarios registrados.");
        else
            foreach (var u in lista)
                Console.WriteLine($"  [{(u.Activo ? "ACTIVO" : "INACTIVO")}] {u.Nombre} | Doc: {u.Documento} | Email: {u.Email}");
        Pause();
    }

    static void ViewUserDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE USUARIO ===\n");
        Console.Write("Ingrese documento: ");
        string doc = Console.ReadLine()!;
        var usuario = usuarioService.BuscarPorDocumento(doc);
        if (usuario == null) Console.WriteLine("Usuario no encontrado.");
        else Console.WriteLine(usuario.DetalleCompleto());
        Pause();
    }

    // =========================
    // SUBMENÚ ACTUALIZAR USUARIO
    // =========================

    static void UpdateUserMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR USUARIO ===");
            Console.WriteLine("1. Editar nombre");
            Console.WriteLine("2. Editar contacto");
            Console.WriteLine("3. Activar / desactivar");
            Console.WriteLine("0. Volver");

            Console.Write("Seleccione una opción: ");
            if (!int.TryParse(Console.ReadLine(), out option)) option = 0;

            switch (option)
            {
                case 1: EditUserName(); break;
                case 2: EditUserContact(); break;
                case 3: ToggleUserActiveStatus(); break;
            }

        } while (option != 0);
    }

    static void EditUserName()
    {
        Console.Clear();
        Console.WriteLine("=== EDITAR NOMBRE ===\n");
        Console.Write("Documento del usuario: ");
        string doc = Console.ReadLine()!;
        var usuario = usuarioService.BuscarPorDocumento(doc);
        if (usuario == null) { Console.WriteLine("Usuario no encontrado."); Pause(); return; }
        Console.Write("Nuevo nombre: ");
        usuario.Nombre = Console.ReadLine()!;
        Console.WriteLine("Nombre actualizado correctamente.");
        Pause();
    }

    static void EditUserContact()
    {
        Console.Clear();
        Console.WriteLine("=== EDITAR CONTACTO ===\n");
        Console.Write("Documento del usuario: ");
        string doc = Console.ReadLine()!;
        var usuario = usuarioService.BuscarPorDocumento(doc);
        if (usuario == null) { Console.WriteLine("Usuario no encontrado."); Pause(); return; }
        Console.Write("Nuevo email: ");
        usuario.Email = Console.ReadLine()!;
        Console.WriteLine("Email actualizado correctamente.");
        Pause();
    }

    static void ToggleUserActiveStatus()
    {
        Console.Clear();
        Console.WriteLine("=== ACTIVAR / DESACTIVAR USUARIO ===\n");
        Console.Write("Ingrese documento: ");
        string doc = Console.ReadLine()!;
        var usuario = usuarioService.BuscarPorDocumento(doc);
        if (usuario == null) { Console.WriteLine("Usuario no encontrado."); Pause(); return; }
        usuario.Activo = !usuario.Activo;
        Console.WriteLine($"Usuario '{usuario.Nombre}' ahora está {(usuario.Activo ? "ACTIVO" : "INACTIVO")}.");
        Pause();
    }

    static void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===\n");
        Console.Write("Ingrese documento del usuario a eliminar: ");
        string doc = Console.ReadLine()!;
        usuarioService.EliminarUsuario(doc);
        Pause();
    }

    // =========================
    // PRESTAMOS
    // =========================

    static void ShowLoansMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("MENU PRESTAMOS");
            Console.WriteLine("1. Crear prestamo");
            Console.WriteLine("2. Listar prestamos");
            Console.WriteLine("3. Ver detalle");
            Console.WriteLine("4. Registrar devolucion");
            Console.WriteLine("5. Eliminar prestamo");
            Console.WriteLine("0. Volver");

            if (!int.TryParse(Console.ReadLine(), out option)) option = 0;

            switch (option)
            {
                case 1: CreateLoan(); break;
                case 2: ListLoansMenu(); break;
                case 3: ViewLoanDetail(); break;
                case 4: RegisterReturn(); break;
                case 5: DeleteLoan(); break;
            }

        } while (option != 0);
    }

    static void CreateLoan()
    {
        Console.Clear();
        Console.WriteLine("=== CREAR PRÉSTAMO ===\n");
        Console.Write("ISBN del libro   : ");
        string isbn = Console.ReadLine()!;
        var libro = libroService.BuscarPorISBN(isbn);
        if (libro == null) { Console.WriteLine("Libro no encontrado."); Pause(); return; }
        if (!libro.Disponible) { Console.WriteLine("El libro no está disponible."); Pause(); return; }

        Console.Write("Documento usuario: ");
        string doc = Console.ReadLine()!;
        var usuario = usuarioService.BuscarPorDocumento(doc);
        if (usuario == null) { Console.WriteLine("Usuario no encontrado."); Pause(); return; }

        Console.Write("Días de préstamo : ");
        if (!int.TryParse(Console.ReadLine(), out int dias)) dias = 7;

        libro.Disponible = false;
        var prestamo = new Prestamo(libro, usuario, DateTime.Now, DateTime.Now.AddDays(dias));
        prestamoService.AgregarPrestamo(prestamo);
        Pause();
    }

    static void ListLoansMenu()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE PRÉSTAMOS ===\n");
        var lista = prestamoService.ObtenerTodos();
        if (lista.Count == 0)
            Console.WriteLine("No hay préstamos registrados.");
        else
            foreach (var p in lista)
                Console.WriteLine($"  [{p.Estado}] {p.Libro?.Titulo} → {p.Usuario?.Nombre} | Vence: {p.FechaLimite:dd/MM/yyyy}");
        Pause();
    }

    static void ViewLoanDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE PRÉSTAMO ===\n");
        Console.Write("Ingrese ISBN del libro: ");
        string isbn = Console.ReadLine()!;
        var prestamo = prestamoService.ObtenerTodos()
            .FirstOrDefault(p => p.Libro?.ISBN == isbn);
        if (prestamo == null) Console.WriteLine("Préstamo no encontrado.");
        else Console.WriteLine(prestamo.DetalleCompleto());
        Pause();
    }

    static void RegisterReturn()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR DEVOLUCIÓN ===\n");
        Console.Write("ISBN del libro a devolver: ");
        string isbn = Console.ReadLine()!;
        var prestamo = prestamoService.ObtenerTodos()
            .FirstOrDefault(p => p.Libro?.ISBN == isbn && p.Estado == EstadoPrestamo.Activo);
        if (prestamo == null) { Console.WriteLine("Préstamo activo no encontrado."); Pause(); return; }
        prestamo.Estado = EstadoPrestamo.Devuelto;
        prestamo.FechaDevolucion = DateTime.Now;
        if (prestamo.Libro != null) prestamo.Libro.Disponible = true;
        Console.WriteLine("Devolución registrada correctamente.");
        Pause();
    }

    static void DeleteLoan()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR PRÉSTAMO ===\n");
        Console.Write("Ingrese ISBN del libro del préstamo: ");
        string isbn = Console.ReadLine()!;
        var prestamo = prestamoService.ObtenerTodos()
            .FirstOrDefault(p => p.Libro?.ISBN == isbn);
        if (prestamo == null) { Console.WriteLine("Préstamo no encontrado."); Pause(); return; }
        if (prestamo.Libro != null) prestamo.Libro.Disponible = true;
        prestamoService.EliminarPrestamo(prestamo);
        Pause();
    }

    // ====================
    // BUSQUEDAS Y REPORTES
    // ====================

    static void ShowSearchReportsMenu()
    {
        Console.Clear();
        Console.WriteLine("MENU BUSQUEDAS Y REPORTES");
        Console.WriteLine("1. Buscar libro");
        Console.WriteLine("2. Buscar usuario");
        Console.WriteLine("3. Reportes");
        Console.WriteLine("0. Volver");

        if (!int.TryParse(Console.ReadLine(), out int option)) option = 0;

        switch (option)
        {
            case 1: SearchBook(); break;
            case 2: SearchUser(); break;
            case 3: ReportsMenu(); break;
        }
    }

    static void SearchBook()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR LIBRO ===\n");
        Console.WriteLine("1. Por ISBN");
        Console.WriteLine("2. Por título");
        Console.WriteLine("3. Por autor");
        Console.Write("Opción: ");
        string op = Console.ReadLine()!;

        if (op == "1")
        {
            Console.Write("ISBN: ");
            var libro = libroService.BuscarPorISBN(Console.ReadLine()!);
            if (libro == null) Console.WriteLine("No encontrado.");
            else Console.WriteLine(libro.DetalleCompleto());
        }
        else if (op == "2")
        {
            Console.Write("Título: ");
            var libros = libroService.BuscarPorTitulo(Console.ReadLine()!);
            if (libros.Count == 0) Console.WriteLine("No encontrado.");
            else foreach (var l in libros) Console.WriteLine($"  [{l.Id}] {l.Titulo} - {l.Autor}");
        }
        else if (op == "3")
        {
            Console.Write("Autor: ");
            var libros = libroService.BuscarPorAutor(Console.ReadLine()!);
            if (libros.Count == 0) Console.WriteLine("No encontrado.");
            else foreach (var l in libros) Console.WriteLine($"  [{l.Id}] {l.Titulo} - {l.Autor}");
        }
        Pause();
    }

    static void SearchUser()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR USUARIO ===\n");
        Console.WriteLine("1. Por documento");
        Console.WriteLine("2. Por nombre");
        Console.Write("Opción: ");
        string op = Console.ReadLine()!;

        if (op == "1")
        {
            Console.Write("Documento: ");
            var usuario = usuarioService.BuscarPorDocumento(Console.ReadLine()!);
            if (usuario == null) Console.WriteLine("No encontrado.");
            else Console.WriteLine(usuario.DetalleCompleto());
        }
        else if (op == "2")
        {
            Console.Write("Nombre: ");
            var usuarios = usuarioService.BuscarPorNombre(Console.ReadLine()!);
            if (usuarios.Count == 0) Console.WriteLine("No encontrado.");
            else foreach (var u in usuarios) Console.WriteLine($"  {u.Nombre} | Doc: {u.Documento}");
        }
        Pause();
    }

    static void ReportsMenu()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTES ===\n");
        libroService.MostrarEstadisticas();
        usuarioService.MostrarEstadisticas();
        prestamoService.MostrarEstadisticas();
        Pause();
    }

    // ============
    // PERSISTENCIA
    // ============

    static void ShowPersistenceMenu()
    {
        Console.WriteLine("1. Guardar datos");
        Console.WriteLine("2. Cargar datos");
        Console.WriteLine("3. Reiniciar datos");

        if (!int.TryParse(Console.ReadLine(), out int option)) option = 0;

        switch (option)
        {
            case 1: SaveData(); break;
            case 2: LoadData(); break;
            case 3: ResetData(); break;
        }
    }

    static void SaveData()
    {
        Console.WriteLine("Datos guardados");
        Pause();
    }

    static void LoadData()
    {
        Console.WriteLine("Datos cargados");
        Pause();
    }

    static void ResetData()
    {
        Console.WriteLine("Datos reiniciados");
        Pause();
    }

    // ======
    // SALIDA
    // ======

    static void ConfirmExitAndSave()
    {
        Console.WriteLine("Desea guardar antes de salir? (S/N)");
        string r = Console.ReadLine()!;

        if (r.ToUpper() == "S")
        {
            SaveData();
        }

        Console.WriteLine("Saliendo del sistema...");
    }

    static void Pause()
    {
        Console.WriteLine("Presione una tecla...");
        Console.ReadKey();
    }
}