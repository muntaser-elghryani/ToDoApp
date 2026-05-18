using FluentValidation;
using ToDoApp.DAL.Repository.Implementations;
using ToDoApp.Dtos.UserDtos;

namespace ToDoApp.Api.Validators
{
    public class CreatUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreatUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("User name is required");

            // 2️⃣ Phone:
            // يبدأ بـ 091 أو 092 أو 093 أو 094
            // ويتكون من 10 أرقام
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required")
                .Matches(@"^(091|092|093|094)\d{7}$")
                .WithMessage("Phone must start with 091, 092, 093, or 094 and be 10 digits long");

            // 3️⃣ Password: لا يقل عن 6
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters");

          

         

          
        }
    }
}
