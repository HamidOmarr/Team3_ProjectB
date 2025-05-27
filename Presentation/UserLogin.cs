namespace Team3_ProjectB
{
    static class UserLogin
    {
        static private AccountsLogic accountsLogic = new AccountsLogic();

        public static AccountModel? Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the login page");
            Console.WriteLine("Please enter your email address:");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            string password = AccountsLogic.ReadPassword();

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
                Console.WriteLine("No account found with that email and password.");
                return null;
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

            string password;
            bool passwordComplex;

            do
            {
                Console.WriteLine("Please enter your password: \n(Password should contain atleast one special character, \none normal and one capital letter and a number and it can't \nbe less than 8 charachters)");
                password = AccountsLogic.ReadPassword();
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

            Menu.Start();
        }


    }
}