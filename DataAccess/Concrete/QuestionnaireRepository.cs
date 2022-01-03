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
        public Questionnaire Add(Questionnaire entity, Guid UserId)
        {
            var rawSql = "INSERT INTO Questionnaires (Id, Name, Description, IsHidden, Status, CreatedAt, UpdatedAt) " +
                "VALUES (@Id, @Name, @Description, @IsHidden, @Status, @CreatedAt, @UpdatedAt)";

            var rawSql2 = "INSERT INTO QuestionnaireUser (QuestionnairesId, UsersId) VALUES (@Id,@UserId)";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, entity);
                if (affectedRows == 0)
                    return null;
                affectedRows = connection.Execute(rawSql2, new {Id = entity.Id, UserId = UserId});
                if (affectedRows == 0)
                    return null;
                return entity;
            }
        }

        public Questionnaire Add(Questionnaire entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            var rawSql = "DELETE FROM Questionnaires WHERE Questionnaires.Id = @Id";
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
            var rawSql = "SELECT * FROM Questionnaires WHERE Questionnaires.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var questionnaire = connection.QueryFirstOrDefault<Questionnaire>(rawSql, new { Id = id });
                return questionnaire;
            }
        }

        public Questionnaire GetWithQuestions(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
               var QuestionnaireDictionary = new Dictionary<Guid, Questionnaire>();
                var result = connection.Query<Questionnaire, Question, Questionnaire>(rawSql,
                    (questionnaire, question) =>
                    {
                        Questionnaire questionnaireEntry;
                        if (!QuestionnaireDictionary.TryGetValue(questionnaire.Id, out questionnaireEntry))
                        {
                            questionnaireEntry = questionnaire;
                            questionnaireEntry.Questions = new List<Question>();
                            QuestionnaireDictionary.Add(questionnaireEntry.Id, questionnaireEntry);
                        }
                        if (question != null)
                            questionnaireEntry.Questions.Add(question);
                        return questionnaireEntry;
                    }
                    , parameters).Distinct().ToList().FirstOrDefault();
                return result;
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
            var rawSql = "UPDATE Questionnaires " +
                "SET Name = @Name, Description = @Description, IsHidden = @IsHidden, Status = @Status, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt " +
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
