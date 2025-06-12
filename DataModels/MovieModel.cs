namespace Team3_ProjectB
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public string Rating { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string SubtitleLanguage { get; set; }

        public MovieModel() { }

        public MovieModel(int id, string title, string description, int durationMinutes, DateOnly releaseDate, string rating, string genre, string language, string subtitleLanguage)
        {
            Id = id;
            Title = title;
            Description = description;
            DurationMinutes = durationMinutes;
            ReleaseDate = releaseDate;
            Rating = rating;
            Genre = genre;
            Language = language;
            SubtitleLanguage = subtitleLanguage;



        }
    }
}