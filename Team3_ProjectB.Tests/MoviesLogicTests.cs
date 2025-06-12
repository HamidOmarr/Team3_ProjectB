namespace Team3_ProjectB.Tests
{
    [TestClass]
    public class MoviesLogicTests
    {
        private string testTitle = "UnitTest Movie";
        private string testDescription = "A movie for unit testing.";
        private int testDuration = 100;
        private DateOnly testReleaseDate = new DateOnly(2024, 1, 1);
        private string testRating = "PG";
        private string testGenre = "TestGenre";
        private string testLanguage = "EN";
        private string testSubtitleLanguage = "NL";

        private long InsertTestMovie()
        {
            MoviesLogic.AddMovie(testTitle, testDescription, testDuration, testReleaseDate, testRating, testGenre, testLanguage, testSubtitleLanguage);
            var logic = new MoviesLogic();
            var allMovies = logic.GetAllMovies();
            var movie = allMovies.LastOrDefault(m => m.Title == testTitle && m.Description == testDescription);
            return movie?.Id ?? 0;
        }

        private void DeleteTestMovie(long id)
        {
            var logic = new MoviesLogic();
            logic.DeleteMovie(id);
        }

        [TestMethod]
        public void AddMovie_ShouldAddMovie()
        {
            // Act
            long id = InsertTestMovie();

            // Assert
            var logic = new MoviesLogic();
            var allMovies = logic.GetAllMovies();
            var movie = allMovies.FirstOrDefault(m => m.Id == id);
            Assert.IsNotNull(movie);
            Assert.AreEqual(testTitle, movie.Title);

            // Clean up
            DeleteTestMovie(id);
        }

        [TestMethod]
        public void GetAllMovies_ShouldReturnMovies()
        {
            // Arrange
            long id = InsertTestMovie();

            // Act
            var logic = new MoviesLogic();
            var allMovies = logic.GetAllMovies();

            // Assert
            Assert.IsTrue(allMovies.Any(m => m.Id == id));

            // Clean up
            DeleteTestMovie(id);
        }

        [TestMethod]
        public void UpdateMovie_ShouldUpdateMovie()
        {
            // Arrange
            long id = InsertTestMovie();
            var logic = new MoviesLogic();
            string newTitle = "Updated UnitTest Movie";

            // Act
            logic.UpdateMovie(id, newTitle, testDescription, testDuration, testReleaseDate, testRating, testGenre, testLanguage, testSubtitleLanguage);

            // Assert
            var allMovies = logic.GetAllMovies();
            var movie = allMovies.FirstOrDefault(m => m.Id == id);
            Assert.IsNotNull(movie);
            Assert.AreEqual(newTitle, movie.Title);

            // Clean up
            DeleteTestMovie(id);
        }

        [TestMethod]
        public void DeleteMovie_ShouldRemoveMovie()
        {
            // Arrange
            long id = InsertTestMovie();
            var logic = new MoviesLogic();

            // Act
            logic.DeleteMovie(id);

            // Assert
            var allMovies = logic.GetAllMovies();
            var movie = allMovies.FirstOrDefault(m => m.Id == id);
            Assert.IsNull(movie);
        }
    }
}
