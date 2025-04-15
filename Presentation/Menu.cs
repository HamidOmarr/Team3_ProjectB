
static class Menu
{
    static public void Start()
    {
        string[] options = { "Login", "View Movies" };
        int selectedIndex = 0;

        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.WriteLine("Use ↑ ↓ to choose, then press Enter:\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                    Console.WriteLine($"> {options[i]}");
                else
                    Console.WriteLine($"  {options[i]}");
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;

        } while (key != ConsoleKey.Enter);

        if (selectedIndex == 0)
            UserLogin.Start();
        else if (selectedIndex == 1)
            //ShowMoviesManager.DisplaySessions();
            ShowMovies.DisplaySessions();

    }

}
