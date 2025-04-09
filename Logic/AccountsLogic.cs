using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class AccountsLogic
{
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        // Could do something here
    }

    public AccountModel? CheckLogin(string email, string password)
    {
        AccountModel acc = AccountsAccess.GetByEmail(email);

        if (acc != null && acc.PasswordHash == password)
        {
            CurrentAccount = acc;
            return acc;
        }

        return null;
    }
}




