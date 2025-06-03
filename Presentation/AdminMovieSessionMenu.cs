using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3_ProjectB;


namespace Team3_ProjectB.Presentation
{
    public class AdminMovieSessionMenu
    {
        public static void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Admin: Manage Movie Sessions ===");
                Console.WriteLine("1. Add Session");
                Console.WriteLine("2. Edit Session");
                Console.WriteLine("3. Delete Session");
                Console.WriteLine("4. Back");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        AddMoviesession.DisplaySessions();
                        break;
                    case ConsoleKey.D2:
                        EditMovieSession.Display();
                        break;
                    case ConsoleKey.D3:
                        DeleteMovieSession.Display();
                        break;
                    case ConsoleKey.D4:
                        return;
                }
            }
        }
    }
}