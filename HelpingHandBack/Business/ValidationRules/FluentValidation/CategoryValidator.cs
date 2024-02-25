using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation {
    public class CategoryValidator : AbstractValidator<Category> {
        public CategoryValidator() {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.Name).Length(2, 40);
        }
    }
}
