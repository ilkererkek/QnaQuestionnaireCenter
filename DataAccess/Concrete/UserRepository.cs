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
    public class UserRepository : IUserRepository
    {

        private readonly IConfig _config;
        public UserRepository(IConfig config)
        {
            _config = config;
        }
        public User Add(User entity)
        {
            var rawSql = "INSERT INTO Users (Id, Name, Surname, Organization, Occupation," +
                "UserRole, PhoneNumber, Email, PasswordHash, PasswordSalt, CreatedAt, UpdatedAt) " +
                "VALUES (@Id, @Name, @Surname, @Organization, @Occupation," +
                "@UserRole, @PhoneNumber, @Email, @PasswordHash, @PasswordSalt, @CreatedAt, @UpdatedAt)";
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
            var rawSql = "DELETE FROM Users WHERE Users.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, new { Id = id });
                if (affectedRows == 0)
                    throw new Exception();
            }
        }

        public User Get(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var user = connection.QueryFirstOrDefault<User>(rawSql, parameters);
                return user;
            }
        }

        public User GetById(Guid id)
        {
            var rawSql = "SELECT * FROM Users WHERE Users.Id = @Id";
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var user = connection.QueryFirstOrDefault<User>(rawSql, new {Id = id});
                return user;
            }
        }

        public List<User> GetList(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var users = connection.Query<User>(rawSql, parameters).ToList();
                return users;
            }
        }

        public List<User> GetListWithQuestionnaires(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var UserDictionary = new Dictionary<Guid, User>();
                var users = connection.Query<User, Questionnaire, User>(rawSql,
                    (user, questionnaire) =>
                    {
                        User userEntry;
                        if (!UserDictionary.TryGetValue(user.Id, out userEntry))
                        {
                            userEntry = user;
                            userEntry.Questionnaires = new List<Questionnaire>();
                            UserDictionary.Add(userEntry.Id, userEntry);
                        }
                        userEntry.Questionnaires.Add(questionnaire);
                        return userEntry;

                    }
                    , parameters, splitOn: "Id")
                    .Distinct()
                    .ToList();
                return users;
            }
        }

        public User GetWithQuestionnaires(object parameters, string rawSql)
        {
            using (SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var UserDictionary = new Dictionary<Guid, User>();
                var user = connection.Query<User, Questionnaire, User>(rawSql,
                    (user, questionnaire) =>
                    {
                        User userEntry;
                        if (!UserDictionary.TryGetValue(user.Id, out userEntry))
                        {
                            userEntry = user;
                            userEntry.Questionnaires = new List<Questionnaire>();
                            UserDictionary.Add(userEntry.Id, userEntry);
                        }
                        userEntry.Questionnaires.Add(questionnaire);
                        return userEntry;

                    }
                    , parameters, splitOn: "Id")
                    .Distinct()
                    .ToList().FirstOrDefault();
                return user;
            }
        }

        public User Update(User entity)
        {
            var rawSql = "UPDATE Users SET Name = @Name, Surname = @Surname, Organization = @Organization, Occupation = @Occupation," +
                "UserRole = @UserRole, PhoneNumber = @PhoneNumber, Email = @Email, PasswordHash = @PasswordHash, " +
                "PasswordSalt = @PasswordSalt, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt" +
                "WHERE Users.Id = @Id";
            using(SqlConnection connection = new SqlConnection(_config.ConnectionString))
            {
                var affectedRows = connection.Execute(rawSql, entity);
                if (affectedRows == 0)
                    return null;
                return entity;
            }
        }
    }
}
