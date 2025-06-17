using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Team3_ProjectB;

namespace Team3_ProjectB
{
    public class MovieSessionsAccess
    {
        private readonly SqliteConnection _connection;
        public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
        {
            public override DateOnly Parse(object value)
                => DateOnly.Parse(value.ToString());

            public override void SetValue(IDbDataParameter parameter, DateOnly value)
                => parameter.Value = value.ToString("yyyy-MM-dd");
        }
        public MovieSessionsAccess()
        {
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            _connection = new SqliteConnection("Data Source=../../../DataSources/ReservationSysteem.db");
            _connection.Open();
        }

        private const string Table = "movie_session";

        public MovieSessionModel GetSessionByMovieAndTime(string movieName, string sessionTime)
        {
            string sql = $@"
            SELECT ms.id AS Id, ms.movie_id AS MovieId, ms.auditorium_id AS AuditoriumId, ms.start_time AS StartTime, ms.end_time AS EndTime
            FROM {Table} ms
            JOIN movie m ON ms.movie_id = m.id
            WHERE m.title = @MovieName AND ms.start_time = @SessionTime";
            var session = _connection.QueryFirstOrDefault<MovieSessionModel>(sql, new { MovieName = movieName, SessionTime = sessionTime });
            if (session != null)
            {
                session.MovieModel = GetMovieById(session.MovieId);
                session.Auditorium = GetAuditoriumById(session.AuditoriumId);
            }
            return session;
        }

        public List<MovieSessionModel> GetAllDetailedMovieSessions()
        {
            string sql = @"
            SELECT 
                ms.id AS Id,
                ms.movie_id AS MovieId,
                ms.auditorium_id AS AuditoriumId,
                ms.start_time AS StartTime,
                ms.end_time AS EndTime
            FROM movie_session ms";
            var sessions = _connection.Query<MovieSessionModel>(sql).ToList();
            foreach (var session in sessions)
            {
                session.MovieModel = GetMovieById(session.MovieId);
                session.Auditorium = GetAuditoriumById(session.AuditoriumId);
            }
            return sessions;
        }

        public void AddMovieSession(long movieId, long auditoriumId, DateTime startTime, DateTime endTime)
        {
            string sql = $@"
            INSERT INTO {Table} (movie_id, auditorium_id, start_time, end_time) 
            VALUES (@MovieId, @AuditoriumId, @StartTime, @EndTime)";
            _connection.Execute(sql, new { MovieId = movieId, AuditoriumId = auditoriumId, StartTime = startTime, EndTime = endTime });
        }

        public List<MovieSessionModel> GetDetailedMovieSessionsFromId(long movieId)
        {
            string sql = @"
            SELECT 
                ms.id AS Id,
                ms.movie_id AS MovieId,
                ms.auditorium_id AS AuditoriumId,
                ms.start_time AS StartTime,
                ms.end_time AS EndTime
            FROM movie_session ms
            WHERE ms.movie_id = @movieId";
            var sessions = _connection.Query<MovieSessionModel>(sql, new { movieId }).ToList();
            foreach (var session in sessions)
            {
                session.MovieModel = GetMovieById(session.MovieId);
                session.Auditorium = GetAuditoriumById(session.AuditoriumId);
            }
            return sessions;
        }

        public List<MovieSessionModel> GetAllMovieSessions()
        {
            string sql = @"
            SELECT 
                ms.id AS Id,
                ms.movie_id AS MovieId,
                ms.auditorium_id AS AuditoriumId,
                ms.start_time AS StartTime,
                ms.end_time AS EndTime
            FROM movie_session ms";
            var movieSessions = _connection.Query<MovieSessionModel>(sql).ToList();
            foreach (var session in movieSessions)
            {
                session.MovieModel = GetMovieById(session.MovieId);
                session.Auditorium = GetAuditoriumById(session.AuditoriumId);
            }
            return movieSessions;
        }
        public void UpdateMovieSession(long sessionId, DateTime newStartTime, DateTime newEndTime)
        {
            string sql = $"UPDATE {Table} SET start_time = @StartTime, end_time = @EndTime WHERE id = @SessionId";
            _connection.Execute(sql, new { SessionId = sessionId, StartTime = newStartTime, EndTime = newEndTime });
        }

        public void DeleteMovieSession(long sessionId)
        {
            string sql = $"DELETE FROM {Table} WHERE id = @SessionId";
            _connection.Execute(sql, new { SessionId = sessionId });
        }

        private MovieModel GetMovieById(int movieId)
        {
            string sql = @"SELECT * FROM movie WHERE id = @Id";
            return _connection.QueryFirstOrDefault<MovieModel>(sql, new { Id = movieId });
        }

        private Auditorium GetAuditoriumById(int auditoriumId)
        {
            string sql = @"SELECT * FROM auditorium WHERE id = @Id";
            return _connection.QueryFirstOrDefault<Auditorium>(sql, new { Id = auditoriumId });
        }
    }
}
