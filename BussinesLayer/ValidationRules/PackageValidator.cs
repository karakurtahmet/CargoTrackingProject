using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class PackageValidator:AbstractValidator<Package>
    {
        public PackageValidator()
        {
            RuleFor(x=>x.Barcode).NotEmpty().WithMessage("Barkod numarasını giriniz");
            
            
        }
    }
}
