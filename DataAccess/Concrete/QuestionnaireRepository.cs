using Dapper;
using DataAccess.Abstract;
using DataAccess.Abstract.Dapper;
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
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private IConfiguration _configuration;
        public QuestionnaireRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(Questionnaire entity)
        {
            var sql = "INSERT INTO Questionnaires (Id, Name, Description, IsHidden, Status, CreatedAt, UpdatedAt) " +
                "values(@Id, '@Name', @Description, @IsHidden, @Status, @CreatedAt, @UpdatedAt)";
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                var affectedRows = connection.Execute(sql, entity);
                if (affectedRows == 0)
                    throw new Exception($"Affected rows are {affectedRows}.\nQuery: \"{sql}\"");
            }

            throw new NotImplementedException();
        }

        public void Delete(Guid Id)
        {
            var sql = "DELETE FROM Questionnaires WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                var affectedRows = connection.Execute(sql, new {Id});
                if (affectedRows == 0)
                    throw new Exception($"Affected rows are {affectedRows}.\nQuery: \"{sql}\"");
            }

            throw new NotImplementedException();
        }

        public Questionnaire? Get(object parameters , string RawSql = "SELECT * FROM Questionnaires WHERE Id = @Id")
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                return connection.Query<Questionnaire>(RawSql,parameters).FirstOrDefault();
            }
        }

        public IList<Questionnaire> GetList(object parameters, string RawSql = "SELECT * FROM Questionnaires")
        {
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                return connection.Query<Questionnaire>(RawSql, parameters).ToList();
            }
        }

        public IList<Questionnaire> GetListWithQuestions(object parameters, string RawSql = "SELECT * FROM Questionnaires LEFT JOIN ON ")
        {
            throw new NotImplementedException();
        }

        public IList<Questionnaire> GetListWithQuestionsAndUsers(object parameters, string RawSql)
        {
            throw new NotImplementedException();
        }

        public IList<Questionnaire> GetListWithUsers(object parameters, string RawSql)
        {
            throw new NotImplementedException();
        }

        public Questionnaire? GetWithQuestions(object parameters, string RawSql)
        {
            throw new NotImplementedException();
        }

        public Questionnaire? GetWithQuestionsandUsers(object parameters, string RawSql)
        {
            throw new NotImplementedException();
        }

        public Questionnaire? GetWithUsers(object parameters, 
            string RawSql = "SELECT * FROM Questionnaires LEFT JOIN QuestionnaireUser QU on Questionnaires.Id = QU.QuestionnairesId " +
                            "LEFT JOIN Users U on Questionnaires.Name = U.Name")
        {
           
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                var QuestionnaireDict = new Dictionary<Guid, Questionnaire>();
            }

            throw new NotImplementedException();
        }

        public void Update(Questionnaire entity)
        {
            var sql = "UPDATE Questionnaires SET Id = @Id, Name = @Name, Description = @Description, IsHidden = @IsHidden, Status = @Status, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt";
            using (var connection = new SqlConnection(_configuration.ConnectionString))
            {
                var affectedRows = connection.Execute(sql, entity);
                if (affectedRows == 0)
                    throw new Exception($"Affected rows are {affectedRows}.\nQuery: \"{sql}\"");
            }

            throw new NotImplementedException();
        }
    }
}
