using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAnswerService
    {
        MultiAnswer AddMultiAnswer(MultiAnswer multiAnswer);
        OptionAnswer AddOptionAnswer(OptionAnswer optionAnswer);
        MultiAnswer MultiAnswerGetById(Guid id);
        OptionAnswer OptionAnswerGetById(Guid id);
        List<MultiAnswer> GetMultiByQuestionId(Guid id);
        List<OptionAnswer> GetOptionByQuestionId(Guid id);
    }
}
