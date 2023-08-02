using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ValidationRules
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Giriniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Alanı Boş Bırakılamaz");
            RuleFor(x => x.FirmId).NotEmpty().WithMessage("Firma Alanı Boş Bırakılamaz");
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı Boş Bırakılamaz");


        }
    }
}
