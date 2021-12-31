using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IQuestionnaireService
    {
        Questionnaire Add(Questionnaire questionnaire);

        bool Delete(Guid id);
        Questionnaire Update(Guid id);
        Questionnaire GetById(Guid Id);
        List<Questionnaire> GetAll();
        Questionnaire GetWithQuestions();

    }
}
