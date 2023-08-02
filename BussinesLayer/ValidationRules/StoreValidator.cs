using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class StoreValidator:AbstractValidator<Store>
    {
        public StoreValidator() 
        {
            RuleFor(x=>x.StoreName).NotEmpty().WithMessage("Mağazanın ismini giriniz");
        }
    }
}
