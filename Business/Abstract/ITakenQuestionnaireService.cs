using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITakenQuestionnaireService
    {
        TakenQuestionnaire Add(TakenQuestionnaire takenQuestionnaire);

        void AddMultiSelection(MultiSelection multiSelection);
        void AddOptionSelection(OptionSelection optionSelection);
        void AddOpenAnswer(OpenAnswer openAnswer);
        void Finish(Guid Id);
        TakenQuestionnaire GetById(Guid Id);


    }
}
