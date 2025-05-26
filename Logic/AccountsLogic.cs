using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Team3_ProjectB
{
    public class AccountsLogic
    {
        public static AccountModel? CurrentAccount { get; private set; }

        public AccountsLogic()
        {
            // Could do something here
        }

        public long WriteAccount(AccountModel account)
        {
            return AccountsAccess.Write(account);
        }

        public AccountModel GetAccountByEmail(string email)
        {
            return AccountsAccess.GetByEmail(email);
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[0..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }


        public static void SetCurrentAccount(AccountModel account)
        {
            CurrentAccount = account;
        }

        public AccountModel? CheckLogin(string email, string password)
        {
            AccountModel acc = AccountsAccess.GetByEmail(email);

            if (acc != null && BCrypt.Net.BCrypt.Verify(password, acc.PasswordHash))
            {
                CurrentAccount = acc;
                return acc;
            }

            return null;
        }
        public void RegisterAccount(AccountModel account)
        {
            try
            {
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(account.PasswordHash);
                AccountsAccess.Write(account);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the account.", ex);
            }
        }

    }
}