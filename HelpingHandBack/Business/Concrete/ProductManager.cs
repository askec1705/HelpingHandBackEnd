using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
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
    public class ProductManager : IProductService {
        IProductDal _productDal;
        IProductImageService _productImageService;
        public ProductManager(IProductDal productDal, IProductImageService productImageService) {
            _productDal = productDal;
            _productImageService = productImageService;

        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("admin,helper")]
        public IDataResult<int> Add(Product product) {
            _productDal.Add(product);
            product = _productDal.Get(p =>
                p.CategoryId == product.CategoryId
            );
            return new SuccessDataResult<int>(product.Id, Messages.ProductAdded);
        }

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("admin,helper")]
        public IResult Delete(Product product) {
            _productImageService.DeleteByProductId(product.Id);
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> Get(int id) {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == id));
        }

        public IDataResult<ProductDetailDto> GetProductDetail(int productId) {
            return new SuccessDataResult<ProductDetailDto>(_productDal.GetProductDetail(productId));
        }


        public IDataResult<List<Product>> GetAll() {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<List<ProductDetailDto>> GetDetails() {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        [SecuredOperation("admin,helper")]
        public IResult Update(Product car) {
            _productDal.Update(car);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
