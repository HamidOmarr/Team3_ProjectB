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
                string? email = AccountsLogic.CustomInput("Please enter your email address (Backspace to go back): ");
                if (email == null) { NavigationService.GoBack(); return null; }

                string? password = AccountsLogic.CustomInput("Please enter your password (Backspace to go back): ", maskInput: true);
                if (password == null) { NavigationService.GoBack(); return null; }

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
                    Console.WriteLine("No account found with that email and password. Please try again.");
                }
            }
        }

        public static void Register()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the registration page");

            string? name = AccountsLogic.CustomInput("Please enter your name (Backspace to go back): ");
            if (name == null) { NavigationService.GoBack(); return; }

            string email;
            while (true)
            {
                string? inputEmail = AccountsLogic.CustomInput("Please enter your email address (Backspace to go back): ");
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
                string? inputPassword = AccountsLogic.CustomInput(
                    "Please enter your password (Backspace to go back):\n" +
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
