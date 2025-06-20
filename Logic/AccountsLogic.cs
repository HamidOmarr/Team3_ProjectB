using System.Text.RegularExpressions;

namespace Team3_ProjectB
{
    public class AccountsLogic
    {
        public static AccountModel? CurrentAccount { get; private set; }
        public static bool IsLoggedIn { get; set; } = false;
        public static bool IsAdmin { get; set; } = false;

        public long WriteAccount(AccountModel account)
        {
            return AccountsAccess.Write(account);
        }

        public AccountModel GetAccountByEmail(string email)
        {
            return AccountsAccess.GetByEmail(email);
        }

        public static bool CheckPasswordComplexity(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;
            string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~";
            if (!password.Any(c => specialChars.Contains(c))) return false;
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
                IsAdmin = acc.AccountType == "admin";
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
        public AccountModel GetOrCreateGuest(string name, string email)
        {
            var existing = GetAccountByEmail(email);
            if (existing != null && existing.AccountType == "guest")
            {
                if (existing.Name != name)
                {
                    existing.Name = name;
                    AccountsAccess.Write(existing);
                }
                return existing;
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("guest_password");
            var guest = new AccountModel(0, name, email, hashedPassword, "guest");
            guest.Id = WriteAccount(guest);
            return guest;
        }
    }
}
