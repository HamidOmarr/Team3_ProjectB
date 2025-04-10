static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address:");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password:");
        string password = Console.ReadLine();

        AccountModel acc = accountsLogic.CheckLogin(email, password);

        if (acc != null)
        {
            Console.WriteLine($"Welcome back, {acc.Name}!");
            Console.WriteLine($"Your email address is: {acc.Email}");
            Console.WriteLine($"Your account type is: {acc.AccountType}");

            Menu.Start();
            ShowMoviesManager.DisplaySessions();


        }
        else
        {
            Console.WriteLine("No account found with that email and password.");
        }
    }
}
