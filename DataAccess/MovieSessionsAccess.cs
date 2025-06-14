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
            return _connection.QueryFirstOrDefault<MovieSessionModel>(sql, new { MovieName = movieName, SessionTime = sessionTime });
        }

        public List<FullMovieSessionModel> GetAllDetailedMovieSessions()
        {
            string sql = @"
            SELECT 
                ms.id AS Id,
                m.title AS Title,
                m.genre AS Genre,
                m.description AS Description,
                ms.start_time AS StartTime,
                ms.end_time AS EndTime,
                a.name AS AuditoriumName
            FROM movie_session ms
            JOIN movie m ON ms.movie_id = m.id
            JOIN auditorium a ON ms.auditorium_id = a.id";

            var sessions = _connection.Query<FullMovieSessionModel>(sql).ToList();
            return sessions;
        }

        public void AddMovieSession(long movieId, long auditoriumId, DateTime startTime, DateTime endTime)
        {
            string sql = $@"
            INSERT INTO {Table} (movie_id, auditorium_id, start_time, end_time) 
            VALUES (@MovieId, @AuditoriumId, @StartTime, @EndTime)";
            _connection.Execute(sql, new { MovieId = movieId, AuditoriumId = auditoriumId, StartTime = startTime, EndTime = endTime });
        }

        public List<FullMovieSessionModel> GetDetailedMovieSessionsFromId(long movieId)
        {
            string sql = @"
            SELECT 
                ms.id AS Id,
                m.title AS Title,
                m.genre AS Genre,
                m.description AS Description,
                ms.start_time AS StartTime,
                ms.end_time AS EndTime,
                a.name AS AuditoriumName,
                ms.auditorium_id AS AuditoriumId
            FROM movie_session ms
            JOIN movie m ON ms.movie_id = m.id
            JOIN auditorium a ON ms.auditorium_id = a.id
            WHERE ms.movie_id = @movieId";

            var sessions = _connection.Query<FullMovieSessionModel>(sql, new { movieId }).ToList();
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
                ms.end_time AS EndTime,
                m.title AS Title,
                m.genre AS Genre,
                m.description AS Description,
                a.name AS AuditoriumName
            FROM movie_session ms
            JOIN movie m ON ms.movie_id = m.id
            JOIN auditorium a ON ms.auditorium_id = a.id";

            var movieSessions = _connection.Query<MovieSessionModel>(sql).ToList();
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
    }
}