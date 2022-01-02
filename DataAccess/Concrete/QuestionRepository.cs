using Dapper;
using DataAccess.Abstract;
using DataAccess.Utils;
using Entity.Concrete;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IConfig _config;
        public QuestionRepository(IConfig config)
        {
            _config = config;
        }
        public Question Add(Question entity)
        {
            var rawSql = "INSERT Into Questions (Id, Text, OrderNo, Type, QuestionnaireId, CreatedAt, UpdatedAt) " +
                "VALUES (@Id, @Text, @OrderNo, @Type, @QuestionnaireId, @CreatedAt, @UpdatedAt)";
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
            throw new NotImplementedException();
        }

        public Question Get(object parameters, string rawSql)
        {
            throw new NotImplementedException();
        }

        public Question GetById(Guid id)
        {
            var rawSql = "SELECT * FROM Questions WHERE Questions.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var question = connection.Query<Question>(rawSql, new { Id = id}).FirstOrDefault();
                
                return question;
            }
        }

        public List<Question> GetList(object parameters, string rawSql)
        {
            throw new NotImplementedException();
        }

        public Question Update(Question entity)
        {
            throw new NotImplementedException();
        }
    }
}
