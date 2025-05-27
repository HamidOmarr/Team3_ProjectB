namespace Team3_ProjectB.DataAccess;

public interface IAccountsAccess
{
    AccountModel GetByEmail(string email);
}
