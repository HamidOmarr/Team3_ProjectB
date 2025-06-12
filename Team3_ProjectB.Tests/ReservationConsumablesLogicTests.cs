namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class ReservationConsumablesLogicTests
    {
        [TestMethod]
        public void GetConsumablesForCheckout_ShouldReturnList()
        {
            var logic = new ReservationConsumablesLogic();
            long testReservationId = 1;
            var result = logic.GetConsumablesForCheckout(testReservationId);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<(string, int, decimal)>));
        }
    }
}
