using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal) {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category) {
            var businessResult = BusinessRules.Run(CheckIfCategoryExists(category));
            if (businessResult != null) {
                return businessResult;
            }
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Category category) {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        [CacheAspect(10)]
        public IDataResult<Category> Get(int categoryId) {
            return new SuccessDataResult<Category>(_categoryDal.Get(b => b.Id == categoryId));
        }

        [CacheAspect(10)]
        public IDataResult<List<Category>> GetAll() {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }
        
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(Category category) {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }

        private IResult CheckIfCategoryExists(Category category) {
            if (_categoryDal.Get(b => b.Name == category.Name) != null) {
                return new ErrorResult(Messages.CategoryAlreadyExists);
            }
            return new SuccessResult();
        }

    }
}
