namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class ConsumablesLogicTests
    {
        [TestMethod]
        public void GetAllConsumables_ShouldReturnConsumables()
        {
            var logic = new ConsumablesLogic();
            var consumables = logic.GetAllConsumables();
            Assert.IsNotNull(consumables);
            Assert.IsTrue(consumables.Count > 0);
        }
    }
}
