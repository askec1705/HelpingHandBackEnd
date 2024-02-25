using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase {
        IProductImageService _productImageManager;

        public ProductImagesController(IProductImageService productImageManager) {
            _productImageManager = productImageManager;
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            return Ok(_productImageManager.GetAll());
        }

        [HttpGet("getbyproductid")]
        public IActionResult GetByProductId(int productId) {
            var images = _productImageManager.GetByProductId(productId);
            if (images.Success) {
                return Ok(images);
            }
            return BadRequest(images);
        }


        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile image, int productId) {
            var result = _productImageManager.Add(image, new ProductImage { ProductId = productId });
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        
        [HttpPost("addmultiple")]
        public IActionResult AddMultiple([FromForm] IFormFile[] images, [FromForm] int productId) {
            var result = _productImageManager.AddMultiple(images, new ProductImage { ProductId = productId });
            
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("update")]
        public IActionResult Update(IFormFile file, [FromForm] ProductImage productImage) {
            var result = _productImageManager.Update(file, productImage);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(ProductImage productImage) {
            var result = _productImageManager.Delete(productImage);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }



    }
}
