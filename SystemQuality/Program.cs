using System;

class Program
{
    static void Main() { ShowMainMenu(); }

    static void ShowMainMenu()
    {
        Console.WriteLine("=== INICIANDO SISTEMA BIBLIOTECA ===");
        Pause();
    }

    static void Pause()
    {
        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }
}