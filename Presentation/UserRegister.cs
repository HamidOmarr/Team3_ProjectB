static class UserRegister
{
    public static void Register()
    {
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
            AccountsAccess.Write(newAccount);
            Console.WriteLine("Account successfully registered!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while registering the account: {ex.Message}");
        }

        // Return to the menu
        Menu.Start();
    }
}
