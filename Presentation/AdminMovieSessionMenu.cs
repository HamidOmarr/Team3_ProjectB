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
            string[] options = { "Add Session", "Edit Session", "Delete Session", "Back" };
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Admin: Manage Movie Sessions ===");
                Console.WriteLine("Use ↑ ↓ to choose, then press Enter:\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"[>]  {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"[ ]  {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                    selectedIndex++;

            } while (key != ConsoleKey.Enter);

            switch (selectedIndex)
            {
                case 0:
                    AddMoviesession.DisplaySessions();
                    break;
                case 1:
                    EditMovieSession.Display();
                    break;
                case 2:
                    DeleteMovieSession.Display();
                    break;
                case 3:
                    return;
            }

            // After action completes, show the menu again
            Show();
        }
    }
}