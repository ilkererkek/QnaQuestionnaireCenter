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
    public class MultiSelectionRepository : IMultiSelectionRepository
    {
        private readonly IConfig _config;
        public MultiSelectionRepository(IConfig config)
        {
            _config = config;
        }
        public MultiSelection Add(MultiSelection entity)
        {
            var rawSql = "insert into MultiSelections " +
                "(Id, MultiAnswerId, TakenQuestionnaireId, CreatedAt, UpdatedAt) " +
                "values (@Id, @MultiAnswerId, @TakenQuestionnaireId, @CreatedAt, @UpdatedAt); ";
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

        public MultiSelection Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.QueryFirstOrDefault<MultiSelection>(rawSql, parameters);
            }
        }

        public MultiSelection GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MultiSelection> GetByQuestionId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MultiSelection> GetByTakenQuestionnaireId(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MultiSelection> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                return connection.Query<MultiSelection>(rawSql, parameters).ToList();
            }
        }

        public MultiSelection Update(MultiSelection entity)
        {
            throw new NotImplementedException();
        }
    }
}
