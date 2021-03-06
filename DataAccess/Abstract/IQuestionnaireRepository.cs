using DataAccess.Abstract.Dapper;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IQuestionnaireRepository : IDapperRepository<Questionnaire>
    {
        Questionnaire Add(Questionnaire questionnaire, Guid UserId);
        Questionnaire GetWithQuestions(object parameters, string rawSql);
    }
}
