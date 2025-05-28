using Microsoft.VisualStudio.TestTools.UnitTesting;
using Team3_ProjectB;
namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class AccountsLogicTests
    {
        private AccountModel CreateTestAccount(string email = "testuser@example.com")
        {
            return new AccountModel(
                id: 0,
                name: "Test User",
                email: email,
                passwordHash: "testpassword",
                accountType: "User"
            );
        }

        [TestMethod]
        public void WriteAccount_ShouldWriteAccountToDatabase()
        {
            // Arrange
            var logic = new AccountsLogic();
            var account = CreateTestAccount("writeaccount@example.com");

            // Act
            logic.WriteAccount(account);

            // Assert
            var result = AccountsAccess.GetByEmail(account.Email);
            Assert.IsNotNull(result);

            // Clean up
            AccountsAccess.Delete(result);
        }

        [TestMethod]
        public void WriteAccount_ShouldReturnNewId()
        {
            // Arrange
            var logic = new AccountsLogic();
            var account = CreateTestAccount();

            // Act
            long newId = logic.WriteAccount(account);

            // Assert
            Assert.IsTrue(newId > 0);

            // Clean up
            account.Id = newId;
            AccountsAccess.Delete(account);
        }

        [TestMethod]
        public void GetAccountByEmail_ShouldReturnCorrectAccount()
        {
            // Arrange
            var logic = new AccountsLogic();
            var account = CreateTestAccount("getbyemail@example.com");
            account.Id = logic.WriteAccount(account);

            // Act
            var result = logic.GetAccountByEmail(account.Email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(account.Email, result.Email);

            // Clean up
            AccountsAccess.Delete(account);
        }

        [TestMethod]
        public void CheckLogin_WithCorrectCredentials_ShouldReturnAccount()
        {
            // Arrange
            var logic = new AccountsLogic();
            var account = CreateTestAccount("login@example.com");
            account.Id = logic.WriteAccount(account);

            // Act
            var result = logic.CheckLogin(account.Email, account.PasswordHash);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(account.Email, result.Email);

            // Clean up
            AccountsAccess.Delete(account);
        }

        [TestMethod]
        public void CheckLogin_WithIncorrectCredentials_ShouldReturnNull()
        {
            // Arrange
            var logic = new AccountsLogic();
            var account = CreateTestAccount("wronglogin@example.com");
            account.Id = logic.WriteAccount(account);

            // Act
            var result = logic.CheckLogin(account.Email, "wrongpassword");

            // Assert
            Assert.IsNull(result);

            // Clean up
            AccountsAccess.Delete(account);
        }

        [TestMethod]
        public void RegisterAccount_ShouldInsertAccount()
        {
            // Arrange
            var logic = new AccountsLogic();
            var account = CreateTestAccount("register@example.com");

            // Act
            logic.RegisterAccount(account);

            // Assert
            var result = AccountsAccess.GetByEmail(account.Email);
            Assert.IsNotNull(result);
            Assert.AreEqual(account.Email, result.Email);

            // Clean up
            AccountsAccess.Delete(result);
        }

        [TestMethod]
        public void SetCurrentAccount_SetsTheCurrentAccount()
        {
            // Arrange
            var account = CreateTestAccount("setcurrent@example.com");

            // Act
            AccountsLogic.SetCurrentAccount(account);

            // Assert
            Assert.IsNotNull(AccountsLogic.CurrentAccount);
            Assert.AreEqual(account.Email, AccountsLogic.CurrentAccount.Email);
        }

    }
}
