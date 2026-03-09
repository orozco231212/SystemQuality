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
}
