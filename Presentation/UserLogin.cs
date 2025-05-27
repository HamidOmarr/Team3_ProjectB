namespace Team3_ProjectB
{
    static class UserLogin
    {
        static private AccountsLogic accountsLogic = new AccountsLogic();

        public static AccountModel? Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the login page (Escape to cancel)");
            Console.WriteLine("Please enter your email address:");
            string email = ReadInputOrEscape();
            if (email == null) return null;

            Console.WriteLine("Please enter your password:");
            string password = ReadPasswordOrEscape();
            if (password == null) return null;

            AccountModel acc = accountsLogic.CheckLogin(email, password);

            if (acc != null)
            {
                Console.WriteLine($"Welcome back, {acc.Name}!");
                Console.WriteLine($"Your email address is: {acc.Email}");
                Console.WriteLine($"Your account type is: {acc.AccountType}");
                return acc;
            }
            else
            {
                Console.WriteLine("No account found with that email and password.");
                return null;
            }
        }

        public static void Register()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the registration page (Escape to cancel)");

            Console.WriteLine("Please enter your name:");
            string name = ReadInputOrEscape();
            if (name == null) return;

            Console.WriteLine("Please enter your email address:");
            string email = ReadInputOrEscape();
            if (email == null) return;

            Console.WriteLine("Please enter your password:");
            string password = ReadPasswordOrEscape();
            if (password == null) return;

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

            Menu.Start();
        }

        private static string ReadInputOrEscape()
        {
            string input = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    Menu.Start();
                    return null;
                }
                else if (key.Key == ConsoleKey.Enter && input.Length > 0)
                {
                    Console.WriteLine();
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }

        private static string ReadPasswordOrEscape()
        {
            string password = "";
            ConsoleKeyInfo key;

            while (true)
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    Menu.Start();
                    return null;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return password;
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
        }
    }
}