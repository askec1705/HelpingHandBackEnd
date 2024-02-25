using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;
        

        public ProductImageManager(IProductImageDal productImageDal) {
            _productImageDal = productImageDal;
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin,helper")]
        public IResult Add(IFormFile formFile, ProductImage productImage) {
            var result = BusinessRules.Run(CheckIfImageCountOfProductExceeded(productImage.ProductId, 1));
            if(result != null) {
                return result;
            }

            string imageName = string.Format(@"{0}.jpg", Guid.NewGuid());
            productImage.ImagePath = Paths.ProductImagePath + imageName;
            productImage.Date = DateTime.Now;

            FileHelper.Write(formFile, Paths.RootPath + productImage.ImagePath);


            _productImageDal.Add(productImage);
            return new SuccessResult(Messages.ImageAdded);
        }

        [SecuredOperation("admin,helper")]
        public IResult AddMultiple(IFormFile[] files, ProductImage productImage) {
            var result = BusinessRules.Run(CheckIfImageCountOfProductExceeded(productImage.ProductId, files.Length));
            if (result != null) {
                return result;
            }
            foreach (var file in files) {
                string imageName = string.Format(@"{0}.jpg", Guid.NewGuid());
                productImage.ImagePath = Paths.ProductImagePath + imageName;
                productImage.Date = DateTime.Now;
                productImage.Id = 0;

                FileHelper.Write(file, Paths.RootPath + productImage.ImagePath);

                _productImageDal.Add(productImage);
            }
            return new SuccessResult(Messages.ImagesAdded);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin,helper")]
        public IResult Delete(ProductImage productImage) {
            string path = _productImageDal.Get(ci => ci.Id == productImage.Id).ImagePath;

            _productImageDal.Delete(productImage);
            FileHelper.Delete(Paths.RootPath + path); 


            return new SuccessResult(Messages.ImageDeleted);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("admin,helper")]
        public IResult DeleteByProductId(int productId) {
            var result = GetByProductId(productId);
            if (result.Success) {
                foreach (var item in result.Data) {
                    Delete(item);
                }
            }
            return new SuccessResult();
        }

        [CacheAspect(60)]
        [PerformanceAspect(1)]
        public IDataResult<List<ProductImage>> GetAll() {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll());
        }

        public IDataResult<List<ProductImage>> GetByProductId(int productId) {
            var images = _productImageDal.GetAll(pi => pi.ProductId == productId);
            return new SuccessDataResult<List<ProductImage>>(images);
        }

        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        [SecuredOperation("admin,helper")]
        public IResult Update(IFormFile formFile, ProductImage productImage) {
            var imageToUpdate = _productImageDal.Get(ci => ci.Id == productImage.Id); // Finding image
            if(imageToUpdate == null) {
                return new ErrorResult(Messages.ImageNotFound);
            }
            productImage.ProductId = imageToUpdate.ProductId;
            productImage.Date = DateTime.Now;
            productImage.ImagePath = imageToUpdate.ImagePath;

            _productImageDal.Update(productImage);

            FileHelper.Write(formFile, Paths.RootPath + imageToUpdate.ImagePath); // Overwriting file

            return new SuccessResult();
        }

        private IResult CheckIfImageCountOfProductExceeded(int productId, int imagesToAdd) {
            if (_productImageDal.GetAll(pi => pi.ProductId == productId).Count + imagesToAdd <= 5) {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.ProductImageCountExceeded);
        }


    }
}
