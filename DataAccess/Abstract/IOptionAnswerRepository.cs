using DataAccess.Abstract.Dapper;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOptionAnswerRepository : IDapperRepository<OptionAnswer>
    {
        List<OptionAnswer> GetByQuestionId(Guid id);
    }
}
