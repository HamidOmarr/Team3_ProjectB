
static class Menu
{
    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to register");
        Console.WriteLine("Enter 3 to view movie sessions");
        Console.WriteLine("Enter 4 to exit");

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            UserRegister.Register();
        }
        else if (input == "3")
        {
            ShowMoviesManager.DisplaySessions();
        }
        else if (input == "4")
        {
            Environment.Exit(0);
        }

        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }
    }
}
