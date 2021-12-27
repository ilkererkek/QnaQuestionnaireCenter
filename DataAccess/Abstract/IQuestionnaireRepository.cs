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
        Questionnaire? GetWithUsers( object parameters, string RawSql);
        Questionnaire? GetWithQuestions(object parameters, string RawSql);
        Questionnaire? GetWithQuestionsandUsers(object parameters, string RawSql);

        IList<Questionnaire> GetListWithQuestions(object parameters, string RawSql);
        IList<Questionnaire> GetListWithUsers(object parameters,string RawSql);
        IList<Questionnaire> GetListWithQuestionsAndUsers(object parameters,string RawSql);
    }
}
