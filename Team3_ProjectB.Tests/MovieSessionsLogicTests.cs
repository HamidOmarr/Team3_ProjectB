namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class MovieSessionsLogicTests
    {
        private long testMovieId = 1;
        private int testAuditoriumId = 1;
        private DateTime testStartTime = new DateTime(2025, 1, 1, 18, 0, 0);
        private DateTime testEndTime = new DateTime(2025, 1, 1, 20, 0, 0);

        private long InsertTestSession()
        {
            var logic = new MovieSessionsLogic();
            logic.AddMovieSession(testMovieId, testAuditoriumId, testStartTime, testEndTime);
            var allSessions = logic.GetAllMovieSessions();
            var session = allSessions.LastOrDefault(s =>
                s.MovieId == testMovieId &&
                s.AuditoriumId == testAuditoriumId &&
                s.StartTime == testStartTime &&
                s.EndTime == testEndTime);
            return session?.Id ?? 0;
        }

        private void DeleteTestSession(long sessionId)
        {
            var logic = new MovieSessionsLogic();
            logic.DeleteMovieSession(sessionId);
        }

        [TestMethod]
        public void AddMovieSession_ShouldAddSession()
        {
            // Act
            long sessionId = InsertTestSession();

            // Assert
            var logic = new MovieSessionsLogic();
            var allSessions = logic.GetAllMovieSessions();
            var session = allSessions.FirstOrDefault(s => s.Id == sessionId);
            Assert.IsNotNull(session);
            Assert.AreEqual(testMovieId, session.MovieId);

            // Clean up
            DeleteTestSession(sessionId);
        }

        [TestMethod]
        public void GetAllMovieSessions_ShouldReturnSessions()
        {
            // Arrange
            long sessionId = InsertTestSession();

            // Act
            var logic = new MovieSessionsLogic();
            var allSessions = logic.GetAllMovieSessions();

            // Assert
            Assert.IsTrue(allSessions.Any(s => s.Id == sessionId));

            // Clean up
            DeleteTestSession(sessionId);
        }

        [TestMethod]
        public void UpdateMovieSession_ShouldUpdateSession()
        {
            // Arrange
            long sessionId = InsertTestSession();
            var logic = new MovieSessionsLogic();
            DateTime newStart = testStartTime.AddHours(1);
            DateTime newEnd = testEndTime.AddHours(1);

            // Act
            logic.UpdateMovieSession(sessionId, newStart, newEnd);

            // Assert
            var allSessions = logic.GetAllMovieSessions();
            var session = allSessions.FirstOrDefault(s => s.Id == sessionId);
            Assert.IsNotNull(session);
            Assert.AreEqual(newStart, session.StartTime);
            Assert.AreEqual(newEnd, session.EndTime);

            // Clean up
            DeleteTestSession(sessionId);
        }

        [TestMethod]
        public void DeleteMovieSession_ShouldRemoveSession()
        {
            // Arrange
            long sessionId = InsertTestSession();
            var logic = new MovieSessionsLogic();

            // Act
            logic.DeleteMovieSession(sessionId);

            // Assert
            var allSessions = logic.GetAllMovieSessions();
            var session = allSessions.FirstOrDefault(s => s.Id == sessionId);
            Assert.IsNull(session);
        }
    }
}
