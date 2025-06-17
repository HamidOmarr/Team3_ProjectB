using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Team3_ProjectB
{
    public class AccountsLogic
    {
        public static AccountModel? CurrentAccount { get; private set; }
        public static bool IsLoggedIn { get; set; } = false;
        public static bool IsAdmin { get; set; } = false;
        public AccountsLogic()
        {
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

        public static bool CheckPasswordComplexity(string password)
        {
            if (password.Length < 8)
                return false;

            if (!password.Any(char.IsUpper))
                return false;

            if (!password.Any(char.IsLower))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~";
            if (!password.Any(c => specialChars.Contains(c)))
                return false;

            return true;
        }

        public static void SetCurrentAccount(AccountModel account)
        {
            CurrentAccount = account;
        }
        public static void Logout()
        {
            CurrentAccount = null;
            IsLoggedIn = false;
            IsAdmin = false;
        }


        public AccountModel? CheckLogin(string email, string password)
        {
            AccountModel acc = AccountsAccess.GetByEmail(email);

            if (acc != null && BCrypt.Net.BCrypt.Verify(password, acc.PasswordHash))
            {
                CurrentAccount = acc;
                IsLoggedIn = true;

                if (acc.AccountType == "admin")
                {
                    IsAdmin = true;
                }
                else if (acc.AccountType == "normal")
                {
                    IsAdmin = false;
                }
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

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return Regex.IsMatch(
                email,
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
            );
        }


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
                        return null;
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

        public AccountModel GetOrCreateGuest(string name, string email)
        {
            var existing = GetAccountByEmail(email);
            if (existing != null)
            {
                return existing;
            }

            var guest = new AccountModel(0, name, email, "guest_password", "guest");
            guest.Id = WriteAccount(guest);
            return guest;
        }
    }
}
