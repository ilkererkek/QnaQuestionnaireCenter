using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IQuestionService
    {
        Question Add(Question question);
        Question Update(Question question);
        bool Delete(Guid id);
        List<Question> GetAll();
        List<Question> GetAllByQuestionnaireId(Guid Id);
        Question GetById(Guid id);
    }
}
