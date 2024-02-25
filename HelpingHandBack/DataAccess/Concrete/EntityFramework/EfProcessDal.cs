using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework {
    public class EfProcessDal : EfEntityRepositoryBase<Process, HelpingHandDbContext>, IProcessDal {
        public List<ProcessDetailDto> GetProcessDetails() {
            using (HelpingHandDbContext context = new()) {
                var result = from process in context.Processes
                             join product in context.Products on process.ProductId equals product.Id
                             join category in context.Categories on product.CategoryId equals category.Id
                             join user in context.Users on process.CustomerId equals user.Id
                             select new ProcessDetailDto {
                                 Id = process.Id,
                                 CategoryName = category.Name,
                                 ProductName = product.ProductName,
                                 CustomerName = user.FirstName + " " + user.LastName,
                             };
                return result.ToList();
            }
        }
    }
}
