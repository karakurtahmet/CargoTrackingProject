using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class DemandValidator:AbstractValidator<Demand>
    {
        public DemandValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().WithMessage("Miktarı Giriniz");
            RuleFor(x => x.Quantity).NotNull().WithMessage("Sayı Giriniz");
            
        }
    }
}
