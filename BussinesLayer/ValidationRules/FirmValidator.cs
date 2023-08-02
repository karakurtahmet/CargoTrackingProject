using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class FirmValidator:AbstractValidator<Firm>
    {
        public FirmValidator()
        {
            RuleFor(x=>x.FirmName).NotEmpty().WithMessage("Firma Adı Boş Olamaz!");
            RuleFor(x=>x.FirmAddres).NotEmpty().WithMessage("Firma Adresi Giriniz!");
            
            
        }
    }
}
