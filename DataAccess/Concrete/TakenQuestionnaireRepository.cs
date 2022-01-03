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
    public class TakenQuestionnaireRepository : ITakenQuestionnaireRepository
    {
        private readonly IConfig _config;
        public TakenQuestionnaireRepository(IConfig config)
        {
            _config = config;
        }
        public TakenQuestionnaire Add(TakenQuestionnaire entity)
        {
            var rawSql = "insert into TakenQuestionnaires (Id, QuestionnaireId, StartTime, FinishTime, CreatedAt, UpdatedAt) " +
                "values  (@Id, @QuestionnaireId, @StartTime, @FinishTime, @CreatedAt, @UpdatedAt)";
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

        public TakenQuestionnaire Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<TakenQuestionnaire>(rawSql, parameters);

            }
        }

        public TakenQuestionnaire GetById(Guid id)
        {
            var rawSql = "SELECT * FROM TakenQuestionnaires WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<TakenQuestionnaire>(rawSql, new { Id = id});
               
            }
        }

        public List<TakenQuestionnaire> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<TakenQuestionnaire>(rawSql, parameters).ToList();

            }
        }

        public TakenQuestionnaire GetWithSelections(Guid Id)
        {
            throw new NotImplementedException();
        }

        public TakenQuestionnaire Update(TakenQuestionnaire entity)
        {
            var rawSql = "UPDATE TakenQuestionnaires SET " +
                "QuestionnaireId = @QuestionnaireId, StartTime = @StartTime, FinishTime = @FinishTime, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt " +
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
