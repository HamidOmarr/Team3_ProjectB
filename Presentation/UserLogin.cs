static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public static void Start()
    {
        Console.Clear();
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
        }
        else
        {
            Console.WriteLine("No account found with that email and password.");
        }
    }
    public static void Register()
    {
        Console.Clear();

        Console.WriteLine("Welcome to the registration page");
        Console.WriteLine("Please enter your name:");
        string name = Console.ReadLine();
        Console.WriteLine("Please enter your email address:");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password:");
        string password = Console.ReadLine();

        AccountModel newAccount = new AccountModel(0, name, email, password, "normal");

        try
        {
            Console.WriteLine("USING DB PATH: " + Path.GetFullPath("DataSources/ReservationSysteem.db"));

            AccountsAccess.Write(newAccount);
            Console.WriteLine("Account successfully registered!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while registering the account: {ex.Message}");
        }

        // Return to the menu
    }
}