using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class ProcessManager : IProcessService
    {
        IProcessDal _processDal;
        IProductService _productService;
        ICustomerService _customerService;
        public ProcessManager(IProcessDal processDal, IProductService productService, ICustomerService customerService) {
            _processDal = processDal;
            _productService = productService;
            _customerService = customerService;
        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Process process) {
            var businessResult = BusinessRules.Run(
                CheckIfProductProcessed(process)
                );
            if (businessResult != null) {
                return businessResult;
            }

            _processDal.Add(process);
            return new SuccessResult(Messages.ProductProcessed);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Process process) {
            _processDal.Delete(process);
            return new SuccessResult();
        }

        [CacheAspect(10)]
        public IDataResult<Process> Get(int userId) {
            return new SuccessDataResult<Process>(_processDal.Get(u => u.Id == userId));
        }

        [SecuredOperation("admin")]
        [CacheAspect(10)]
        public IDataResult<List<Process>> GetAll() {
            return new SuccessDataResult<List<Process>>(_processDal.GetAll());
        }

        [CacheAspect(10)]
        [SecuredOperation("admin")]
        public IDataResult<List<ProcessDetailDto>> GetDetails() {
            return new SuccessDataResult<List<ProcessDetailDto>>(_processDal.GetProcessDetails().OrderByDescending(r => r.PickUpDate).ToList());
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IProcessService.Get")]
        public IResult Update(Process process) {
            _processDal.Update(process);
            return new SuccessResult();
        }

        private IResult CheckIfProductProcessed(Process process) {
            var isOccupied = _processDal.GetAll(p => p.ProductId == process.ProductId
            ).Any();
            if (isOccupied) {
                return new ErrorResult(Messages.ProductAlreadyProcessed);
            }
            return new SuccessResult();
        }
    }
}
