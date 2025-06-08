
using Team3_ProjectB.Logic;

namespace Team3_ProjectB
{
    public static class LoginStatusHelper
    {
        public static void ShowLoginStatus()
        {
            if (AccountsLogic.CurrentAccount != null && AccountsLogic.IsAdmin == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"=== Logged in as: {AccountsLogic.CurrentAccount.Name} ===\n");
                Console.ResetColor();
            }
            else if(AccountsLogic.CurrentAccount != null && AccountsLogic.IsAdmin == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"=== Logged in as: {AccountsLogic.CurrentAccount.Name}. You are logged in as an admin ===\n");
                Console.ResetColor();
            }
        }
    }
}
