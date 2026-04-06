using System;
using SystemQuality.Models;
using SystemQuality.Services;

class Program
{
static LibroService    libroService    = new LibroService();
    static UsuarioService  usuarioService  = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();

    //===============================
    //MENU PRINCIPAL DE LA BIBLIOTECA
    //===============================

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

    // Prueba Libro
   Libro libro = new Libro
{
    Titulo = "Cien años de soledad",
    Autor = "Gabriel García Márquez",
    ISBN = "123456"
};

    Console.WriteLine("\nLibro creado:");
    Console.WriteLine($"Titulo: {libro.Titulo}");
    Console.WriteLine($"Autor: {libro.Autor}");
    Console.WriteLine($"ISBN: {libro.ISBN}");

    // Prueba Usuario
   Usuario usuario = new Usuario
{
    Nombre = "Juan Perez",
    Documento = "123456789",
    Email = "juan@email.com"
};


    Console.WriteLine("\nUsuario creado:");
    Console.WriteLine($"Nombre: {usuario.Nombre}");
    Console.WriteLine($"Documento: {usuario.Documento}");
    Console.WriteLine($"Email: {usuario.Email}");

    // Prueba Prestamo
   Prestamo prestamo = new Prestamo
{
    Libro = libro,
    Usuario = usuario
};
    Console.WriteLine("\nPrestamo creado:");
    Console.WriteLine($"Libro: {prestamo.Libro.Titulo}");
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

    // -- Agregar libros --
    Libro l1 = new Libro("Cien años de soledad", "García Márquez", "ISBN-001");
    Libro l2 = new Libro("El principito", "Saint-Exupéry", "ISBN-002");
    Libro l3 = new Libro("Don Quijote", "Cervantes", "ISBN-003");
    l1.Disponible = false;

    libroService.AgregarLibro(l1);
    libroService.AgregarLibro(l2);
    libroService.AgregarLibro(l3);

    // -- Agregar usuarios --
    Usuario u1 = new Usuario("Ana Torres",  "1001", "ana@mail.com");
    Usuario u2 = new Usuario("Juan Pérez",  "1002", "juan@mail.com");
    Usuario u3 = new Usuario("María López", "1003", "maria@mail.com");
    u3.Activo = false;

    usuarioService.AgregarUsuario(u1);
    usuarioService.AgregarUsuario(u2);
    usuarioService.AgregarUsuario(u3);

    // -- Agregar préstamos --
    Prestamo p1 = new Prestamo(l1, u1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(5));
    Prestamo p2 = new Prestamo(l2, u2, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-3));

    prestamoService.AgregarPrestamo(p1);
    prestamoService.AgregarPrestamo(p2);

    // -- Búsquedas --
    Console.WriteLine("\n-- Búsquedas --");
    var libroBuscado = libroService.BuscarPorISBN("ISBN-002");
    Console.WriteLine($"Libro por ISBN-002: {libroBuscado?.Titulo ?? "No encontrado"}");

    var usuarioBuscado = usuarioService.BuscarPorDocumento("1001");
    Console.WriteLine($"Usuario por doc 1001: {usuarioBuscado?.Nombre ?? "No encontrado"}");

    // -- Ordenación --
    Console.WriteLine("\n-- Libros ordenados por título --");
    foreach (var l in libroService.OrdenarPorTitulo())
        Console.WriteLine($"   {l.Titulo}");

    Console.WriteLine("\n-- Usuarios ordenados por nombre --");
    foreach (var u in usuarioService.OrdenarPorNombre())
        Console.WriteLine($"   {u.Nombre}");

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
        Console.WriteLine("Funcion: Registrar libro");
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
        Console.WriteLine("Mostrando todos los libros...");
        Pause();
    }

    static void ListBooksAvailable()
    {
        Console.WriteLine("Mostrando libros disponibles...");
        Pause();
    }

    static void ListBooksBorrowed()
    {
        Console.WriteLine("Mostrando libros prestados...");
        Pause();
    }

    static void ViewBookDetail()
    {
        Console.WriteLine("Ver detalle del libro por ID/ISBN");
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
            Console.WriteLine("3. Editar año / categoria");
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
        Console.WriteLine("Editar titulo del libro");
        Pause();
    }

    static void EditBookAuthor()
    {
        Console.WriteLine("Editar autor del libro");
        Pause();
    }

    static void EditBookYearCategory()
    {
        Console.WriteLine("Editar año o categoria del libro");
        Pause();
    }

    static void DeleteBook()
    {
        Console.WriteLine("Eliminar libro (validar si esta prestado)");
        Pause();
    }

// =========================
// USUARIOS
// =========================

static void ShowUsersMenu()
{
    Console.Clear();
    Console.WriteLine("2) USUARIOS");
    Console.WriteLine("2.1 Registrar usuario");
    Console.WriteLine("2.2 Listar usuarios");
    Console.WriteLine("2.3 Ver detalle (por ID/documento)");
    Console.WriteLine("2.4 Actualizar usuario");
    Console.WriteLine("2.5 Eliminar usuario");
    Console.WriteLine("0 Volver");

    Console.Write("Seleccione una opción: ");
    int option = int.Parse(Console.ReadLine()!);

    switch (option)
    {
        case 1: RegisterUser(); break;
        case 2: ListUsers(); break;
        case 3: ViewUserDetail(); break;
        case 4: UpdateUserMenu(); break;
        case 5: DeleteUser(); break;
    }
}

static void RegisterUser()
{
    Console.Clear();
    Console.WriteLine("2.1 Registrar usuario");
    Pause();
}

static void ListUsers()
{
    Console.Clear();
    Console.WriteLine("2.2 Listar usuarios");
    Pause();
}

static void ViewUserDetail()
{
    Console.Clear();
    Console.WriteLine("2.3 Ver detalle de usuario");
    Pause();
}

// =========================
// SUBMENÚ ACTUALIZAR USUARIO
// =========================

static void UpdateUserMenu()
{
    Console.Clear();
    Console.WriteLine("2.4 Actualizar usuario");
    Console.WriteLine("1 Editar nombre");
    Console.WriteLine("2 Editar contacto");
    Console.WriteLine("3 Activar / desactivar");
    Console.WriteLine("0 Volver");

    Console.Write("Seleccione una opción: ");
    int option = int.Parse(Console.ReadLine()!);

    switch (option)
    {
        case 1: EditUserName(); break;
        case 2: EditUserContact(); break;
        case 3: ToggleUserActiveStatus(); break;
    }
}

static void EditUserName()
{
    Console.Clear();
    Console.WriteLine("Editar nombre de usuario");
    Pause();
}

static void EditUserContact()
{
    Console.Clear();
    Console.WriteLine("Editar contacto de usuario");
    Pause();
}

static void ToggleUserActiveStatus()
{
    Console.Clear();
    Console.WriteLine("Activar / Desactivar usuario");
    Pause();
}

static void DeleteUser()
{
    Console.Clear();
    Console.WriteLine("2.5 Eliminar usuario");
    Console.WriteLine("Validar: no permitir si tiene préstamos activos");
    Pause();
}

    // =========================
    // PRESTAMOS
    // =========================

    static void ShowLoansMenu()
    {
        Console.WriteLine("Menu Prestamos");
        Console.WriteLine("1 Crear prestamo");
        Console.WriteLine("2 Listar prestamos");
        Console.WriteLine("3 Ver detalle");
        Console.WriteLine("4 Registrar devolucion");
        Console.WriteLine("5 Eliminar prestamo");
        Console.WriteLine("0 Volver");

        int option = int.Parse(Console.ReadLine()!);

        switch (option)
        {
            case 1: CreateLoan(); break;
            case 2: ListLoansMenu(); break;
            case 3: ViewLoanDetail(); break;
            case 4: RegisterReturn(); break;
            case 5: DeleteLoan(); break;
        }
    }

    static void CreateLoan()
    {
        Console.WriteLine("Crear prestamo - Validaciones sugeridas");
        Pause();
    }

    static void ListLoansMenu()
    {
        Console.WriteLine("Listar prestamos");
        Pause();
    }

    static void ViewLoanDetail()
    {
        Console.WriteLine("Ver detalle del prestamo");
        Pause();
    }

    static void RegisterReturn()
    {
        Console.WriteLine("Registrar devolucion");
        Pause();
    }

    static void DeleteLoan()
    {
        Console.WriteLine("Eliminar prestamo");
        Pause();
    }

    // ====================
    // BUSQUEDAS Y REPORTES
    // ====================

    static void ShowSearchReportsMenu()
    {
        Console.WriteLine("Menu Busquedas y Reportes");
        Console.WriteLine("1 Buscar libro");
        Console.WriteLine("2 Buscar usuario");
        Console.WriteLine("3 Reportes");

        int option = int.Parse(Console.ReadLine()!);

        switch (option)
        {
            case 1: SearchBook(); break;
            case 2: SearchUser(); break;
            case 3: ReportsMenu(); break;
        }
    }

    static void SearchBook()
    {
        Console.WriteLine("Buscar libro");
        Pause();
    }

    static void SearchUser()
    {
        Console.WriteLine("Buscar usuario ");
        Pause();
    }


    static void ReportsMenu()
    {
        Console.WriteLine("Menu reportes");
        Pause();
    }

    // ============
    // PERSISTENCIA
    // ============

    static void ShowPersistenceMenu()
    {
        Console.WriteLine("1 Guardar datos");
        Console.WriteLine("2 Cargar datos");
        Console.WriteLine("3 Reiniciar datos");

        int option = int.Parse(Console.ReadLine()!);

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


