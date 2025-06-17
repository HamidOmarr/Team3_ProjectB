namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class ReservationsLogicTests
    {
        private long createdReservationId = 0;
        private long createdUserId = 0;
        private string testEmail = "unit_test_user@example.com";

        private long CreateTestUser()
        {
            var logic = new AccountsLogic();
            var existing = AccountsAccess.GetByEmail(testEmail);
            if (existing != null)
            {
                return existing.Id;
            }
            var account = new AccountModel(
                0,
                "Unit Test",
                testEmail,
                BCrypt.Net.BCrypt.HashPassword("Password1234!"),
                "User"
            );
            return logic.WriteAccount(account);
        }

        [TestMethod]
        public void CreateReservation_ShouldReturnNewId_AndBeRetrievable()
        {
            createdUserId = CreateTestUser();

            var logic = new ReservationsLogic();
            var reservation = new ReservationModel
            {
                UserId = createdUserId,
                TotalPrice = 25.50m,
                Status = "Pending"
            };

            createdReservationId = logic.CreateReservation(reservation);

            Assert.IsTrue(createdReservationId > 0);
            var retrieved = ReservationsAccess.GetById(createdReservationId);
            Assert.IsNotNull(retrieved);
            Assert.AreEqual(reservation.UserId, retrieved.UserId);
            Assert.AreEqual(reservation.TotalPrice, retrieved.TotalPrice);
            Assert.AreEqual(reservation.Status, retrieved.Status);
        }

        [TestMethod]
        public void UpdateReservation_ShouldChangeStatus()
        {
            createdUserId = CreateTestUser();

            var logic = new ReservationsLogic();
            var reservation = new ReservationModel
            {
                UserId = createdUserId,
                TotalPrice = 10.00m,
                Status = "Pending"
            };
            long id = logic.CreateReservation(reservation);

            // Act
            var toUpdate = ReservationsAccess.GetById(id);
            toUpdate.Status = "Confirmed";
            ReservationsAccess.Update(toUpdate);

            // Assert
            var updated = ReservationsAccess.GetById(id);
            Assert.AreEqual("Confirmed", updated.Status);

            // Clean up
            ReservationsAccess.Delete(id);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (createdReservationId > 0)
            {
                ReservationsAccess.Delete(createdReservationId);
                createdReservationId = 0;
            }
            var user = AccountsAccess.GetByEmail(testEmail);
            if (user != null)
            {
                AccountsAccess.Delete(user);
                createdUserId = 0;
            }
        }
    }
}
