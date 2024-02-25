using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs {
    public class ProcessDetailDto {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public DateTime PickUpDate { get; set; }
    }
}
