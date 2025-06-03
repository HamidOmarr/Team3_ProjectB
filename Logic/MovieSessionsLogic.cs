using System;
using System.Collections.Generic;
using Team3_ProjectB;

namespace Team3_ProjectB
{
    public class MovieSessionsLogic
    {
        private readonly MovieSessionsAccess _movieSessionsAccess;

        public MovieSessionsLogic()
        {
            _movieSessionsAccess = new MovieSessionsAccess();
        }

        public List<MovieSessionModel> GetAllMovieSessions()
        {
            return _movieSessionsAccess.GetAllMovieSessions();
        }

        public List<FullMovieSessionModel> GetDetailedMovieSessionsFromId(long movieId)
        {
            return _movieSessionsAccess.GetDetailedMovieSessionsFromId(movieId);
        }

        public List<FullMovieSessionModel> GetAllDetailedMovieSessions()
        {
            return _movieSessionsAccess.GetAllDetailedMovieSessions();
        }

        public MovieSessionModel GetSessionByMovieAndTime(string movieName, string sessionTime)
        {
            return _movieSessionsAccess.GetSessionByMovieAndTime(movieName, sessionTime);
        }

        public void AddMovieSession(long movieId, int auditoriumId, DateTime startTime, DateTime endTime)
        {
            _movieSessionsAccess.AddMovieSession(movieId, auditoriumId, startTime, endTime);
        }

        public void UpdateMovieSession(long sessionId, DateTime newStartTime, DateTime newEndTime)
        {
            _movieSessionsAccess.UpdateMovieSession(sessionId, newStartTime, newEndTime);
        }

        public void DeleteMovieSession(long sessionId)
        {
            _movieSessionsAccess.DeleteMovieSession(sessionId);
        }
    }
}
