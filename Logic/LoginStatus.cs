namespace Team3_ProjectB
{
    public static class LoginStatusHelper
    {
        public static void ShowLoginStatus()
        {
            if (AccountsLogic.CurrentAccount != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"=== Logged in as: {AccountsLogic.CurrentAccount.Name} ===\n");
                Console.ResetColor();
            }
        }
    }
}
