using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class WarehouseValidator:AbstractValidator<Warehouse>
    {
        public WarehouseValidator() 
        {
            RuleFor(x=>x.WarehouseName).NotEmpty().WithMessage("Depo İsmi Boş Olamaz");
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Depo Kapasitesi Boş Olamaz");
        }
    }
}
