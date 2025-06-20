namespace Team3_ProjectB
{
    static class UserLogin
    {
        static private AccountsLogic accountsLogic = new AccountsLogic();

        public static string? CustomInput(string prompt, bool maskInput = false)
        {
            Console.WriteLine(prompt);
            string input = "";
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return string.IsNullOrEmpty(input) ? null : input;
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
                        return null;
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;
                    if (maskInput) Console.Write("*");
                    else Console.Write(keyInfo.KeyChar);
                }
            }
        }

        public static AccountModel? Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the login page");

            while (true)
            {
                string? email = CustomInput("Please enter your email address (Press Backspace to go back): ");
                if (email == null) { NavigationService.GoBack(); return null; }

                var acc = accountsLogic.GetAccountByEmail(email);
                if (acc != null && acc.AccountType == "guest")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sorry, this account doesn't exist.");
                    Console.ResetColor();
                    continue;
                }

                string? password = CustomInput("Please enter your password (Press Backspace to go back): ", maskInput: true);
                if (password == null) { NavigationService.GoBack(); return null; }

                var loggedInAcc = accountsLogic.CheckLogin(email, password);

                if (loggedInAcc != null)
                {
                    Console.WriteLine($"Welcome back, {loggedInAcc.Name}!");
                    Console.WriteLine($"Your email address is: {loggedInAcc.Email}");
                    Console.WriteLine($"Your account type is: {loggedInAcc.AccountType}");
                    return loggedInAcc;
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

            string? name = CustomInput("Please enter your name (Press Backspace to go back): ");
            if (name == null) { NavigationService.GoBack(); return; }

            string email;
            while (true)
            {
                string? inputEmail = CustomInput("Please enter your email address (Press Backspace to go back): ");
                if (inputEmail == null) { NavigationService.GoBack(); return; }
                if (accountsLogic.IsValidEmail(inputEmail))
                {
                    email = inputEmail;
                    break;
                }
                Console.WriteLine("Invalid email format. Please try again.");
            }

            string password;
            bool passwordComplex;
            do
            {
                string? inputPassword = CustomInput(
                    "Please enter your password (Press Backspace to go back):\n" +
                    "(Password should contain at least one special character, one lowercase, one uppercase letter, a number, and be at least 8 characters long)",
                    maskInput: true
                );
                if (inputPassword == null) { NavigationService.GoBack(); return; }

                password = inputPassword;
                passwordComplex = AccountsLogic.CheckPasswordComplexity(password);

                if (!passwordComplex)
                {
                    Console.WriteLine("Password does not meet complexity requirements. Please try again.");
                }
            } while (!passwordComplex);

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
    }
}
