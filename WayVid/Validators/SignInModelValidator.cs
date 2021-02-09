using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Model;

namespace WayVid.Validators
{
    public class SignInModelValidator : AbstractValidator<SignInModel>
    {

        public SignInModelValidator()
        {
            RuleFor(model => model.UserName).NotNull()
                .NotEmpty()
                .Length(4, 25);
            RuleFor(model => model.Password).NotNull()
                .NotEmpty()
                .Length(8, 25);
            RuleFor(model => model.RememberMe).NotNull()
                .NotEmpty();
        }
    }
}
