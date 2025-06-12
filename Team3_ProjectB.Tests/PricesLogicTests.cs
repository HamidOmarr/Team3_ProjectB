namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class PricesLogicTests
    {
        [TestMethod]
        public void GetPrice_ShouldReturnDecimal()
        {
            var logic = new PricesLogic();
            int seatTypeId = 1;
            int? promotionTypeId = null;
            var price = logic.GetPrice(seatTypeId, promotionTypeId);
            Assert.IsInstanceOfType(price, typeof(decimal));
            Assert.IsTrue(price >= 0);
        }
    }
}
