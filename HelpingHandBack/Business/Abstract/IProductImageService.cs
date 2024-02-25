using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract {
    public interface IProductImageService
    {
        IDataResult<List<ProductImage>> GetAll();
        IDataResult<List<ProductImage>> GetByProductId(int productId);
        IResult Add(IFormFile formFile, ProductImage productImage);
        IResult Update(IFormFile formFile, ProductImage productImage);
        IResult DeleteByProductId(int productId);
        IResult Delete(ProductImage productImage);
        IResult AddMultiple(IFormFile[] files, ProductImage productImage);
    }
}
