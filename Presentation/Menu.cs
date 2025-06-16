using Team3_ProjectB;
using Team3_ProjectB.Presentation;

namespace Team3_ProjectB
{
    static class Menu
    {
        static public void Start()
        {
            while (true)
            {
                string[] options;

                bool isLoggedIn = AccountsLogic.IsLoggedIn;
                bool isAdmin = AccountsLogic.IsAdmin;

                if (isLoggedIn && isAdmin)
                {
                    options = new[] { "Manage Movies", "Manage Movie Sessions", "View Movies", "Logout", "Register", "Quit" };
                }
                else if (isLoggedIn)
                {
                    options = new[] { "View Movies", "Logout", "Register", "Quit" };
                }
                else
                {
                    options = new[] { "View Movies", "Login", "Register", "Quit" };
                }

                int selectedIndex = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    LoginStatusHelper.ShowLoginStatus();
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

                if (isLoggedIn && isAdmin)
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            NavigationService.Navigate(AdminMovieMenu.Show);
                            break;
                        case 1:
                            NavigationService.Navigate(AdminMovieSessionMenu.Show);
                            break;
                        case 2:
                            NavigationService.Navigate(ShowMovies.DisplaySessions);
                            break;
                        case 3:
                            AccountsLogic.Logout();
                            Console.WriteLine("You have been logged out. Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        case 4:
                            NavigationService.Navigate(UserLogin.Register);
                            break;
                        case 5:
                            Environment.Exit(0);
                            break;
                    }
                }
                else if (isLoggedIn)
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            NavigationService.Navigate(ShowMovies.DisplaySessions);
                            break;
                        case 1:
                            AccountsLogic.Logout();
                            Console.WriteLine("You have been logged out. Press any key to continue...");
                            Console.ReadKey();
                            continue;
                        case 2:
                            NavigationService.Navigate(UserLogin.Register);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            NavigationService.Navigate(ShowMovies.DisplaySessions);
                            break;
                        case 1:
                            NavigationService.Navigate(() =>
                            {
                                var user = UserLogin.Start();
                                if (user != null)
                                {
                                    AccountsLogic.SetCurrentAccount(user);
                                    Console.WriteLine("You are now logged in. Press any key to continue...");
                                    Console.ReadKey();
                                    NavigationService.Clear();
                                    NavigationService.Navigate(Menu.Start);
                                }
                            });
                            continue;
                        case 2:
                            NavigationService.Navigate(UserLogin.Register);
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
    }
}
