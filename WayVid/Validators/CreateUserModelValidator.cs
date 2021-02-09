using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WayVid.Database.Model;
using WayVid.Infrastructure.Enum;

namespace WayVid.Validators
{
    public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserModelValidator()
        {
            RuleFor(model => model.UserRole)
                .NotEmpty()
                .NotNull()
                .Must(model => new List<RoleType> { RoleType.Owner, RoleType.Visitor }.Contains(model))
                .WithMessage("Only 'Visitor' or 'Owner' role type is allowed.");
            RuleFor(model => model.UserName)
                .NotNull()
                .NotEmpty()
                .Length(4, 25);
            RuleFor(model => model.Password)
                .NotNull()
                .NotEmpty()
                .Length(8, 25);
            RuleFor(model => model.RepeatedPassword)
                .NotNull()
                .NotEmpty()
                .Equal(model => model.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
