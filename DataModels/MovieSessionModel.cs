using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public string StartTime { get; set; }

    [Required]
    public string EndTime { get; set; }

    public virtual MovieModel MovieModel { get; set; }
    public virtual Auditorium Auditorium { get; set; }

    public MovieSessionModel() { }

    public MovieSessionModel(int id, int movieId, int auditoriumId, string startTime, string endTime)
    {
        Id = id;
        MovieId = movieId;
        AuditoriumId = auditoriumId;
        StartTime = startTime;
        EndTime = endTime;
    }
}

