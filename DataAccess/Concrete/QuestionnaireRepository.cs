using Dapper;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Utils;

namespace DataAccess.Concrete
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly IConfig _config;
        public QuestionnaireRepository(IConfig config)
        {
            _config = config;
        }
        public Questionnaire Add(Questionnaire entity)
        {
            var rawSql = "INSERT INTO Questionnaires (Id, Name, Description, IsHidden, Status, CreatedAt, UpdatedAt) " +
                "VALUES (@Id, @Name, @Description, @IsHidden, @Status, @CreatedAt, @UpdatedAt)";
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
            var rawSql = "DELETE FROM Questionnaires WHERE UsersQuestionnairesId = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, new { Id = id });
                if (affectedRows == 0)
                    throw new Exception();
            }
        }

        public Questionnaire Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var questionnaire = connection.QueryFirstOrDefault<Questionnaire>(rawSql, parameters);
                return questionnaire;
            }
        }

        public Questionnaire GetById(Guid id)
        {
            var rawSql = "SELECT * FROM Questionnaire WHERE Questionnaire.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var questionnaire = connection.QueryFirstOrDefault<Questionnaire>(rawSql, new { Id = id });
                return questionnaire;
            }
        }

        public List<Questionnaire> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var questionnaires = connection.Query<Questionnaire>(rawSql, parameters).ToList();
                return questionnaires;
            }
        }
        public Questionnaire Update(Questionnaire entity)
        {
            var rawSql = "UPDATE Questionnaires SET Id = @Id , Name = @Name, Description = @Description, IsHidden = @IsHidden, Status = @Status, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt";
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
