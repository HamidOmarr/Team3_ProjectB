public class AccountModel
{
    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string AccountType { get; set; }

    public AccountModel(Int64 id, string name, string email, string passwordHash, string accountType)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        AccountType = accountType;
    }
}




