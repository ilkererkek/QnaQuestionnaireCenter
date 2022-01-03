using Dapper;
using DataAccess.Abstract;
using DataAccess.Utils;
using Entity.Concrete;
using System.Data.SqlClient;

namespace DataAccess.Concrete
{
    public class OpenAnswerRepository : IOpenAnswerRepository
    {
        private readonly IConfig _config;
        public OpenAnswerRepository(IConfig config)
        {
            _config = config;
        }
        public OpenAnswer Add(OpenAnswer entity)
        {
            var rawSql = "INSERT INTO OpenAnswers (Id, Answer, CreatedAt, UpdatedAt, Title, Text, QuestionId, TakenQuestionnaireId)" +
                "VALUES (@Id, @Answer, @CreatedAt, @UpdatedAt, @Title, @Text, @QuestionId, @TakenQuestionnaireId)";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, entity);
                if (affectedRows == 0)
                    return null;
                return entity;
            }
        }

        public void Delete(Guid id)
        {
            var rawSql = "DELETE FROM OpenAnswers WHERE OpenAnswers.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, new { Id = id });
            }
        }

        public OpenAnswer Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<OpenAnswer>(rawSql, parameters);
            }
        }

        public OpenAnswer GetById(Guid id)
        {
            var rawSql = "SELECT * FROM OpenAnswers WHERE OpenAnswers.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<OpenAnswer>(rawSql, new { Id = id });
            }
        }

        public List<OpenAnswer> GetByQuestionId(Guid id)
        {
            var rawSql = "SELECT * FROM OpenAnswers WHERE OpenAnswers.QuestionId = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<OpenAnswer>(rawSql, new { Id = id }).ToList();
            }
        }

        public List<OptionSelection> GetByTakenQuestionnaireId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<OpenAnswer> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<OpenAnswer>(rawSql, parameters).ToList();
            }
        }

        public OpenAnswer Update(OpenAnswer entity)
        {
            var rawSql = "UPDATE OpenAnswers " +
                "SET Answer = @Answer, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, Title = @Title, Text = @Title, QuestionId = @QuestionId " +
                "WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, entity);
                if (affectedRows == 0)
                    return null;
                return entity;
            }
        }
    }
}
