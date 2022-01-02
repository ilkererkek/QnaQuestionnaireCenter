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
            throw new NotImplementedException();
        }

        public MultiAnswer Get(object parameters, string rawSql)
        {
            throw new NotImplementedException();
        }

        public MultiAnswer GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<MultiAnswer> GetList(object parameters, string rawSql)
        {
            throw new NotImplementedException();
        }

        public MultiAnswer Update(MultiAnswer entity)
        {
            throw new NotImplementedException();
        }
    }
}
