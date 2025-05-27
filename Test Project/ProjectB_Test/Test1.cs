using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Team3_ProjectB;
using Team3_ProjectB.DataAccess;

namespace ProjectB_Test
{
    [TestClass]
    public class AccountsLogicTests
    {
        [TestMethod]
        public void SetCurrentAccount_SetsStaticProperty()
        {
            var account = new AccountModel { Email = "test@test.com" };
            AccountsLogic.SetCurrentAccount(account);

            Assert.AreEqual(account, AccountsLogic.CurrentAccount);
        }

        [TestMethod]
        public void CheckLogin_CorrectCredentials_ReturnsAccount()
        {
            // Arrange: You must ensure this user exists in your test DB with this password.
            var logic = new AccountsLogic();
            var email = "integration@test.com";
            var password = "pw123";
            var account = new AccountModel { Email = email, PasswordHash = password };
            AccountsAccess.Write(account); // Create the account in test DB

            // Act
            var result = logic.CheckLogin(email, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(email, result.Email);
        }

        [TestMethod]
        public void CheckLogin_WrongPassword_ReturnsNull()
        {
            var logic = new AccountsLogic();
            var email = "integration2@test.com";
            var account = new AccountModel { Email = email, PasswordHash = "goodpw" };
            AccountsAccess.Write(account);

            var result = logic.CheckLogin(email, "badpw");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegisterAccount_WritesAccount()
        {
            var logic = new AccountsLogic();
            var email = "registertest@test.com";
            var account = new AccountModel { Email = email, PasswordHash = "pw" };

            logic.RegisterAccount(account);

            var result = AccountsAccess.GetByEmail(email);

            Assert.IsNotNull(result);
            Assert.AreEqual(email, result.Email);
        }
    }
}

