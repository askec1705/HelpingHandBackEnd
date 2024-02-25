using Castle.Core.Internal;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfProductDal : EfEntityRepositoryBase<Product, HelpingHandDbContext>, IProductDal
    {
        public ProductDetailDto GetProductDetail(int carId) {
            using (HelpingHandDbContext context = new HelpingHandDbContext()) {
                var result = from product in context.Products
                             where product.Id == carId
                             join category in context.Categories on product.CategoryId equals category.Id
                             select new ProductDetailDto {
                                 Id = product.Id,
                                 CategoryId = product.CategoryId,
                                 CategoryName = category.Name,
                                 ProductName = product.ProductName,
                                 Address = product.Address,
                                 Description = product.Description,
                                 Images = context.ProductImages.Where(ci => ci.ProductId == product.Id).ToList()
                             };
                var productDetail = result.First();
                if (productDetail.Images.Count == 0)
                    productDetail.Images = new List<ProductImage> { new ProductImage {
                        ProductId = productDetail.Id,
                        ImagePath = "images/default.jpg"
                    } };
                return productDetail;
            }
        }

        public List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null) {
            using (HelpingHandDbContext context = new HelpingHandDbContext()) {
                var result = from product in context.Products
                             join category in context.Categories on product.CategoryId equals category.Id
                             select new ProductDetailDto {
                                 Id = product.Id,
                                 CategoryId = product.CategoryId,
                                 CategoryName = category.Name,
                                 ProductName = product.ProductName,
                                 Address = product.Address,
                                 Description = product.Description,
                                 Images = context.ProductImages.Count(ci => ci.ProductId == product.Id) != 0
                                 ? context.ProductImages.Where(ci => ci.ProductId == product.Id).ToList()
                                 : new List<ProductImage> { new ProductImage {
                                        ProductId = product.Id,
                                        ImagePath = "images/default.jpg"
                                    } }

                             };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
