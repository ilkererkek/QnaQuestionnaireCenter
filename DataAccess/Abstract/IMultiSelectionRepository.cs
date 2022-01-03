using DataAccess.Abstract.Dapper;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IMultiSelectionRepository : IDapperRepository<MultiSelection>
    {
        List<MultiSelection> GetByQuestionId(Guid id);
        List<MultiSelection> GetByTakenQuestionnaireId(Guid id);
    }
}
