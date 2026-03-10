using System;

class Program
{
    //===============================
    //MENU PRINCIPAL DE LA BIBLIOTECA
    //===============================

    static void Main() { ShowMainMenu(); }

    static void ShowMainMenu()
    {
        Console.WriteLine("INICIANDO SISTEMA BIBLIOTECA");
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }
    
    //===================
    //MENU  DE NAVEGACION
    //===================


    static void ShowMainMenu()
    {
        int option;

        do
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA BIBLIOTECA =====");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Prestamos");
            Console.WriteLine("4. Busquedas y Reportes");
            Console.WriteLine("5. Guardar / Cargar Datos");
            Console.WriteLine("6. Salir");

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
                default:
                    Console.WriteLine("Opcion invalida");
                    Pause();
                    break;
            }

        } while (option != 6);
    }

}
