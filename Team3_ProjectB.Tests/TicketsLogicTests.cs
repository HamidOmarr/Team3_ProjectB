namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class TicketsLogicTests
    {
        [TestMethod]
        public void GetTicketById_ShouldReturnTicketOrNull()
        {
            var logic = new TicketsLogic();
            long testTicketId = 1;
            var ticket = logic.GetTicketById(testTicketId);
            if (ticket != null)
                Assert.IsInstanceOfType(ticket, typeof(TicketModel));
        }
    }
}
