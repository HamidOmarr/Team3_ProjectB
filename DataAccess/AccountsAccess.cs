using Microsoft.Data.Sqlite;

using Dapper;


public static class AccountsAccess
{
    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/Reservationsysteem.db");

    private static string Table = "user";

    public static void Write(AccountModel account)
    {
        string sql = $"INSERT INTO {Table} (name, email, password_hash, account_type) VALUES (@Name, @Email, @PasswordHash, @AccountType)";
        _connection.Execute(sql, new
        {
            Name = account.Name,
            Email = account.Email,
            PasswordHash = account.PasswordHash,
            AccountType = account.AccountType
        });
    }

    public static AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT id AS Id, name AS Name, email AS Email, password_hash AS PasswordHash, account_type AS AccountType FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public static void Update(AccountModel account)
    {
        string sql = $"UPDATE {Table} SET name = @Name, email = @Email, password_hash = @PasswordHash, account_type = @AccountType WHERE id = @Id";
        _connection.Execute(sql, new
        {
            Id = account.Id,
            Name = account.Name,
            Email = account.Email,
            PasswordHash = account.PasswordHash,
            AccountType = account.AccountType
        });
    }

    public static void Delete(AccountModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = account.Id });
    }
}
