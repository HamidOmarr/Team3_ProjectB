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

        // Create an instance of AccountsAccess
        AccountsAccess accountsAccess = new AccountsAccess();

        // Generate a unique random ID
        long id;
        Random random = new Random();
        do
        {
            id = random.Next(1, int.MaxValue); // Generate a random ID
        } while (accountsAccess.DoesIdExist(id)); // Ensure the ID does not exist

        AccountModel newAccount = new AccountModel(id, name, email, password, "normal");

        try
        {
            accountsAccess.Write(newAccount); // Call the instance method
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
