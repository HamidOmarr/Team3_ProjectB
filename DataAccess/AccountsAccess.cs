using Microsoft.Data.Sqlite;
using Dapper;

public class AccountsAccess
{
    private static SqliteConnection _connection = new SqliteConnection(@"Data Source=C:\Users\Dell\Downloads\ProjectB\Team3_ProjectB\DataSources\ReservationSysteem.db");
    private static string Table = "user";

    public AccountsAccess()
    {
        _connection.Open();
    }

    public void Write(AccountModel account)
    {
        using (var connection = new SqliteConnection(@"Data Source=C:\Users\Dell\Downloads\ProjectB\Team3_ProjectB\DataSources\ReservationSysteem.db"))
        {
            connection.Open();
            string sql = $"INSERT INTO {Table} (id, name, email, password_hash, account_type) VALUES (@Id, @Name, @Email, @PasswordHash, @AccountType)";
            connection.Execute(sql, new
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                PasswordHash = account.PasswordHash,
                AccountType = account.AccountType
            });
        }
    }


    public AccountModel GetByEmail(string email)
    {
        string sql = $"SELECT id AS Id, name AS Name, email AS Email, password_hash AS PasswordHash, account_type AS AccountType FROM {Table} WHERE email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
    }

    public void Update(AccountModel account)
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

    public void Delete(AccountModel account)
    {
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        _connection.Execute(sql, new { Id = account.Id });
    }

    public bool DoesIdExist(long id)
    {
        string sql = $"SELECT COUNT(1) FROM {Table} WHERE id = @Id";
        return _connection.ExecuteScalar<int>(sql, new { Id = id }) > 0;
    }

}
