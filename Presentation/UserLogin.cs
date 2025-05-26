namespace Team3_ProjectB
{
    static class UserLogin
    {
        static private AccountsLogic accountsLogic = new AccountsLogic();

        public static AccountModel? Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the login page");

            while (true)
            {
                string? email = CustomInput("Please enter your email address (Backspace to go back): ");
                if (email == null) { NavigationService.GoBack(); return null; }

                string? password = CustomInput("Please enter your password (Backspace to go back): ", maskInput: true);
                if (password == null) { NavigationService.GoBack(); return null; }

                AccountModel acc = accountsLogic.CheckLogin(email, password);

                if (acc != null)
                {
                    Console.WriteLine($"Welcome back, {acc.Name}!");
                    Console.WriteLine($"Your email address is: {acc.Email}");
                    Console.WriteLine($"Your account type is: {acc.AccountType}");
                    return acc; // Return the logged-in user
                }
                else
                {
                    Console.WriteLine("No account found with that email and password. Please try again.");
                }
            }
        }

        public static void Register()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the registration page");

            string? name = CustomInput("Please enter your name (Backspace to go back): ");
            if (name == null) { NavigationService.GoBack(); return; }

            string email;
            while (true)
            {
                string? inputEmail = CustomInput("Please enter your email address (Backspace to go back): ");
                if (inputEmail == null) { NavigationService.GoBack(); return; }
                if (accountsLogic.IsValidEmail(inputEmail))
                {
                    email = inputEmail;
                    break;
                }
                Console.WriteLine("Invalid email format. Please try again.");
            }

            string? password = CustomInput("Please enter your password (Backspace to go back): ", maskInput: true);
            if (password == null) { NavigationService.GoBack(); return; }

            AccountModel newAccount = new AccountModel(0, name, email, password, "normal");

            try
            {
                accountsLogic.RegisterAccount(newAccount);
                Console.WriteLine("Account successfully registered!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while registering the account: {ex.Message}");
            }

            NavigationService.Navigate(Menu.Start);
        }

        // Helper for custom input with Backspace support
        private static string? CustomInput(string prompt, bool maskInput = false)
        {
            Console.WriteLine(prompt);
            string input = "";
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine();
                        return input;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        Console.WriteLine();
                        return null; // Signal to go back
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;
                    if (maskInput)
                        Console.Write("*");
                    else
                        Console.Write(keyInfo.KeyChar);
                }
            }
        }
    }
}
