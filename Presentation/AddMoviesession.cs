using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3_ProjectB.Presentation
{
    public class AddMoviesession : ShowMovies
    {
        public static int AuditoriumId;
        public static DateTime StartTime;
        public static DateTime EndTime;
        public new static void DisplaySessions()
        {
            DisplaySessionsCore(selectedMovie =>
            {
                AuditoriumId = Console.ReadLine();
                MovieSessionsAccess.AddMovieSession(selectedMovie.Id, auditoriumId, startTime, endTime);

            });
        }

    }
}
