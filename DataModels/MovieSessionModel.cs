using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team3_ProjectB
{
    public class MovieSessionModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [Required]
        [ForeignKey("Auditorium")]
        public int AuditoriumId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public virtual MovieModel MovieModel { get; set; }
        public virtual Auditorium Auditorium { get; set; }

        public MovieSessionModel() { }

        public MovieSessionModel(int id, int movieId, int auditoriumId, DateTime startTime, DateTime endTime)
        {
            Id = id;
            MovieId = movieId;
            AuditoriumId = auditoriumId;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
