using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs {
    public class ProductDetailDto : IDto {

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<ProductImage> Images { get; set; }
    }
}
