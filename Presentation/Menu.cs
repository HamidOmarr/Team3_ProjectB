using Team3_ProjectB;

namespace Team3_ProjectB
{
    static class Menu
    {
        static public void Start()
        {
            string[] options = { "View Movies", "Login", "Register", "Quit" };
            int selectedIndex = 0;

            ConsoleKey key;
            do
            {
                Console.Clear();
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

            if (selectedIndex == 0)
                NavigationService.Navigate(ShowMovies.DisplaySessions);
            else if (selectedIndex == 1)
            {
                var user = UserLogin.Start();
                if (user != null)
                {
                    AccountsLogic.SetCurrentAccount(user);
                    Console.WriteLine("You are now logged in. Press any key to continue...");
                    Console.ReadKey();
                    NavigationService.Navigate(Menu.Start);
                }
            }
            else if (selectedIndex == 2)
                NavigationService.Navigate(UserLogin.Register);
            else if (selectedIndex == 3)
                Environment.Exit(0);
        }
    }
}

