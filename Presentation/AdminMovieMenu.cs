using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3_ProjectB;


namespace Team3_ProjectB.Presentation
{
    public class AdminMovieMenu
    {
        public static void Show()
        {

            string[] options = { "Add Movie", "Edit Movie", "Delete Movie"};
            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                LoginStatusHelper.ShowLoginStatus();

                Console.WriteLine("=== Admin: Manage Movie ===");
                Console.WriteLine("Use ↑ ↓ to choose, then press Enter: (Press backspace to go back)\n");

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
                else if (key == ConsoleKey.Backspace)
                {
                    NavigationService.Navigate(Menu.Start);
                    return;
                }

            } while (key != ConsoleKey.Enter);

            switch (selectedIndex)
            {
                case 0:
                    AddMovie.AddMovieMethod();
                    break;
                case 1:
                    EditMovie.Display();
                    break;
                case 2:
                    DeleteMovie.Display();
                    break;
            }

            Show();
        }
    }
}