using Dapper;
using Microsoft.Data.Sqlite;

namespace Team3_ProjectB
{
    public static class AccountsAccess
    {
        private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";


        private const string Table = "user";

        public static long Write(AccountModel account)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = $"INSERT INTO {Table} (name, email, password_hash, account_type) VALUES (@Name, @Email, @PasswordHash, @AccountType); SELECT last_insert_rowid();";
            return connection.ExecuteScalar<long>(sql, account); // Return the generated Id
        }

        public static AccountModel GetByEmail(string email)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = $"SELECT id AS Id, name AS Name, email AS Email, password_hash AS PasswordHash, account_type AS AccountType FROM {Table} WHERE email = @Email";
            return connection.QueryFirstOrDefault<AccountModel>(sql, new { Email = email });
        }

        public static void Update(AccountModel account)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = $"UPDATE {Table} SET name = @Name, email = @Email, password_hash = @PasswordHash, account_type = @AccountType WHERE id = @Id";
            connection.Execute(sql, account);
        }

        public static void Delete(AccountModel account)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = $"DELETE FROM {Table} WHERE id = @Id";
            connection.Execute(sql, new { account.Id });
        }
    }
}