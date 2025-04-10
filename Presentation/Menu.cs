
static class Menu
{
    static public void Start()
    {
        Console.WriteLine("Enter 1 to login");
        Console.WriteLine("Enter 2 to view movie sessions");

        string input = Console.ReadLine();
        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            ShowMoviesManager.DisplaySessions();
        }
        else
        {
            Console.WriteLine("Invalid input");
            Start();
        }
    }
}
