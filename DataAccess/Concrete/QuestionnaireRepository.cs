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

        public Questionnaire? Get(object parameters, string where = "Id = @Id", List<string>? include = null)
        {
            if(include == null || include.Count == 0)
            {

            }
            else if (include.Contains("Users") && include.Contains("Questions"))
            {

            }
            else if (include.Contains("Users"))
            {

            }
            else if (include.Contains("Questions"))
            {

            }
            throw new NotImplementedException();
        }

        public IList<Questionnaire> GetList(object parameters, string where = "Id = @Id", List<string>? include = null)
        {
            if (include == null || include.Count == 0)
            {

            }
            else if (include.Contains("Users") && include.Contains("Questions"))
            {

            }
            else if (include.Contains("Users"))
            {

            }
            else if (include.Contains("Questions"))
            {

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
