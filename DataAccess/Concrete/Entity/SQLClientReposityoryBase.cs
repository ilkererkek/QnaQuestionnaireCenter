using DataAccess.Abstract.Entity;
using Entity.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entity
{
    public class SQLClientReposityoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {

        public SQLClientReposityoryBase(string table)
        {
            _table = table;
        }

        private readonly static string _connectionString = "";
        private static string? _table;
        public void Add(TEntity entity)
        {
            using (SqlConnection client = new SqlConnection(_connectionString))
            {

                var query = $"INSERT INTO {_table} (";
                var values = " VALUES (";
                foreach (var property in entity.GetType().GetProperties())
                {
                    query = query + $"{property.Name},";
                    values = values + $"{property.GetValue(entity)},;";
                }
                query = query.Remove(query.Length - 1, 1) + ")" ;
                values = values.Remove(query.Length - 1, 1) + ")" ;



            }
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            using (SqlConnection client = new SqlConnection(_connectionString))
            {

                var query = $"DELETE FORM {_table} WHERE Id = {entity.GetType().GetProperty("Id")};";


            }
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (SqlConnection client = new SqlConnection(_connectionString))
            {

                var where = "";
                var join = "";
                if(filter != null)
                {
                    where = $"WHERE {ParseTree(filter.Body, filter.Parameters[0])}";
                }

            }
            throw new NotImplementedException();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null)
        {
            var where = "";
            var join = "";
            if (filter != null)
            {
                where = $"WHERE {ParseTree(filter.Body, filter.Parameters[0])}";
            }
            throw new NotImplementedException();
        }

        public List<TEntity> GetListWithInclude(Expression<Func<TEntity, bool>>? filter = null, List<string>? includes = null)
        {
            throw new NotImplementedException();
        }

        public TEntity GetWithInclude(Expression<Func<TEntity, bool>>? filter = null, List<string>? includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }


        string ParseTree(Expression ex, object param)
        {
            var final = "";
            var op = "";
            var isBinary = true;
            switch (ex.NodeType)
            {
                case ExpressionType.AndAlso:
                    op = " AND ";
                    break;
                case ExpressionType.OrElse:
                    op = " OR ";
                    break;
                case ExpressionType.GreaterThan:
                    op = " > ";
                    break;
                case ExpressionType.LessThan:
                    op = " < ";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    op = " >= ";
                    break;
                case ExpressionType.LessThanOrEqual:
                    op = " <= ";
                    break;
                case ExpressionType.NotEqual:
                    op = " != ";
                    break;
                case ExpressionType.Equal:
                    op = " = ";
                    break;
                default:
                    isBinary = false;
                    break;
            }
            if (ex.NodeType == ExpressionType.NotEqual)
            {
                var y = 1;
            }
            if (!isBinary && ex.NodeType == ExpressionType.MemberAccess)
            {
                var mex = ex as MemberExpression;

                if (mex.Expression.NodeType == ExpressionType.Parameter)
                {
                    var s = mex.Member.Name;

                    final = $"{_table}.{s} ";
                }
                else
                {
                    var objectMember = Expression.Convert(mex, typeof(object));
                    var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                    var getter = getterLambda.Compile();
                    final = $" '{ getter().ToString()}' ";

                }


            }
            else if (!isBinary && ex.NodeType == ExpressionType.Not)
            {
                var uex = ex as UnaryExpression;
                var inner = "";
                if (uex.Operand != null)
                {
                    inner = ParseTree(uex.Operand, param);
                }
                final = final + " NOT " + inner;
            }
            else if (!isBinary && ex.NodeType == ExpressionType.Constant)
            {
                var cex = ex as ConstantExpression;
                if (cex.Value != null)
                {
                    final = $" '{cex.Value.ToString()}' ";
                }
                else
                {
                    final = " NULL ";
                }
            }
            else if (isBinary)
            {
                var bex = ex as BinaryExpression;
                var left = "";
                var right = "";
                if (bex.Left != null)
                    left = ParseTree(bex.Left, param);
                if (bex.Right != null)
                    right = ParseTree(bex.Right, param);
                final = final + left + op + right;
            }
            return final;
        }
    }
}
