using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        IProductService _productManager;

        public ProductsController(IProductService productManager) {
            _productManager = productManager;
        }

        [HttpGet("get")]
        public IActionResult Get(int id) {
            var result = _productManager.Get(id);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getproductdetail")]
        public IActionResult GetProductDetail(int productId) {
            var result = _productManager.GetProductDetail(productId);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getdetails")]
        public IActionResult GetDetails() {
            var result = _productManager.GetDetails();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll() {
            var result = _productManager.GetAll();
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product) {
            var result = _productManager.Add(product);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product) {
            var result = _productManager.Delete(product);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Product product) {
            var result = _productManager.Update(product);
            if (result.Success) {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
