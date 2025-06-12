using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Team3_ProjectB;

namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class SeatsLogicTests
    {
        private int testAuditoriumId = 1;
        private string testRow = "A";
        private int testSeatNumber = 3;

        [TestMethod]
        public void GetSeatsByAuditorium_ShouldReturnSeats()
        {
            var logic = new SeatsLogic();
            var seats = logic.GetSeatsByAuditorium(testAuditoriumId);
            Assert.IsNotNull(seats);
            Assert.IsTrue(seats.Count > 0);
        }

        [TestMethod]
        public void GetSeatByRowAndNumber_ShouldReturnCorrectSeat()
        {
            var logic = new SeatsLogic();
            var seat = logic.GetSeatByRowAndNumber(testRow, testSeatNumber, testAuditoriumId);

            Assert.IsNotNull(seat);
            Assert.AreEqual(testRow, seat.RowNumber);
            Assert.AreEqual(testSeatNumber, seat.SeatNumber);
        }

        [TestMethod]
        public void GetReservedSeatIds_ShouldReturnHashSet()
        {
            int testMovieSessionId = 1;
            var reservedSeats = SeatsLogic.GetReservedSeatIds(testMovieSessionId);
            Assert.IsNotNull(reservedSeats);
            Assert.IsInstanceOfType(reservedSeats, typeof(HashSet<int>));
        }
    }
}
