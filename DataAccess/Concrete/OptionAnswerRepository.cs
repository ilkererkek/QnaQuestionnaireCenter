using Dapper;
using DataAccess.Abstract;
using DataAccess.Utils;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class OptionAnswerRepository : IOptionAnswerRepository
    {
         private readonly IConfig _config;
        public OptionAnswerRepository(IConfig config)
        {
            _config = config;
        }
        public OptionAnswer Add(OptionAnswer entity)
        {
            var rawSql = "INSERT INTO OptionAnswers (Id, OrderId, CreatedAt, UpdatedAt, Title, Text, QuestionId)" +
                "VALUES (@Id, @OrderId, @CreatedAt, @UpdatedAt, @Title, @Text, @QuestionId)";
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
            var rawSql = "DELETE FROM OptionAnswers WHERE OptionAnswers.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, new {Id= id});
            }
        }

        public OptionAnswer Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<OptionAnswer>(rawSql, parameters);
            }
        }

        public OptionAnswer GetById(Guid id)
        {
            var rawSql = "SELECT * FROM OptionAnswers WHERE OptionAnswers.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<OptionAnswer>(rawSql, new {Id=id});
            }
        }

        public List<OptionAnswer> GetByQuestionId(Guid id)
        {
            var rawSql = "SELECT * FROM OptionAnswers WHERE OptionAnswers.QuestionId = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<OptionAnswer>(rawSql, new { Id = id }).ToList();
            }
        }

        public List<OptionAnswer> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<OptionAnswer>(rawSql, parameters).ToList();
            }
        }

        public OptionAnswer Update(OptionAnswer entity)
        {
            var rawSql = "UPDATE OptionAnswers " +
                "SET OrderId = @OrderId, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, Title = @Title, Text = @Text, QuestionId = @QuestionId " +
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
