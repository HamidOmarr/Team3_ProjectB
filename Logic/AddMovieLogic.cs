using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3_ProjectB.Logic
{
    public class AddMovieLogic
    {

        public static void AddMovieToDB(string title, string description, int duration_minutes, DateOnly release_date, string rating, string genre, string language, string subtitle_language)
        {
            MoviesAccess moviesAccess = new MoviesAccess();
            moviesAccess.AddMovie(
                title,
                description,
                duration_minutes,
                        release_date,
                        rating,
                        genre,
                        language,
                        subtitle_language
                    );
        }



    }
}
