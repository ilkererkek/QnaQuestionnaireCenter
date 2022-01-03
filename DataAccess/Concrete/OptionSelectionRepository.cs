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
    public class OptionSelectionRepository : IOptionSelectionRepository
    {
        private readonly IConfig _config;
        public OptionSelectionRepository(IConfig config)
        {
            _config = config;
        }
        public OptionSelection Add(OptionSelection entity)
        {
            var rawSql = "insert into OptionSelections " +
                "(Id, TakenQuestionnaireId, OptionAnswerId, CreatedAt, UpdatedAt) " +
                "values (@Id, @TakenQuestionnaireId, @OptionAnswerId, @CreatedAt, @UpdatedAt); ";
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

        public OptionSelection Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<OptionSelection>(rawSql, parameters);
            }
        }

        public OptionSelection GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<OptionSelection> GetByQuestionId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<OptionSelection> GetByTakenQuestionnaireId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<OptionSelection> GetList(object parameters, string rawSql)
        {
            throw new NotImplementedException();
        }

        public OptionSelection Update(OptionSelection entity)
        {
            throw new NotImplementedException();
        }
    }
}
