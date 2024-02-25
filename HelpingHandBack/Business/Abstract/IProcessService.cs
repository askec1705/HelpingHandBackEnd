using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract {
    public interface IProcessService {
        IDataResult<List<Process>> GetAll();
        IDataResult<Process> Get(int processId);
        IDataResult<List<ProcessDetailDto>> GetDetails();
        IResult Add(Process process);
        IResult Update(Process process);
        IResult Delete(Process process);
    }
}
