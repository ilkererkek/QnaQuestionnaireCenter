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
    public class MultiAnswerRepository : IMultiAnswerRepository
    {
        private readonly IConfig _config;
        public MultiAnswerRepository(IConfig config)
        {
            _config = config;
        }
        public MultiAnswer Add(MultiAnswer entity)
        {
            var rawSql = "INSERT INTO MultiAnswers (Id, Title, Text, QuestionId, CreatedAt, UpdatedAt)" +
                "VALUES (@Id,@Title,@Text,@QuestionId,@CreatedAt,@UpdatedAt)";
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
            var rawSql = "DELETE FROM MultiAnswers WHERE MultiAnswers.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, new {Id= id});
            }
        }

        public MultiAnswer Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<MultiAnswer>(rawSql, parameters);
            }
        }

        public MultiAnswer GetById(Guid id)
        {
            var rawSql = "SELECT * FROM MultiAnswers WHERE MultiAnswers.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<MultiAnswer>(rawSql, new {Id=id});
            }
        }

        public List<MultiAnswer> GetByQuestionId(Guid id)
        {
            var rawSql = "SELECT * FROM MultiAnswers WHERE MultiAnswers.QuestionId = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<MultiAnswer>(rawSql, new { Id = id }).ToList();
            }
        }

        public List<MultiAnswer> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<MultiAnswer>(rawSql, parameters).ToList();
            }
        }

        public MultiAnswer Update(MultiAnswer entity)
        {
            var rawSql = "UPDATE MultiAnswers " +
                "SET Title = @Title, Text = @Text, QuestionId = @QuestionId, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt " +
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
