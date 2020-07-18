using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace BoardSlide.API.Application.Cards.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator()
        {
            RuleFor(request => request.Name)
               .MaximumLength(32).WithMessage("Name must not exceed 32 characters.");

            RuleFor(request => request.Description)
               .MaximumLength(255).WithMessage("Description must not exceed 255 characters.");
        }

        protected override bool PreValidate(ValidationContext<UpdateCardCommand> context, ValidationResult result)
        {
            var request = context.InstanceToValidate as UpdateCardCommand;

            PropertyInfo[] validatedProperties = typeof(UpdateCardCommand)
                .GetProperties()
                .Where(prop => prop.Name != "BoardId" && prop.Name != "CardListId" && prop.Name != "CardId")
                .ToArray();

            validatedProperties
                .Where(prop => prop.PropertyType == typeof(string))
                .ToList()
                .ForEach(prop =>
                {
                    if (string.IsNullOrWhiteSpace((string)prop.GetValue(request)))
                    {
                        prop.SetValue(request, null);
                    }
                });

            if (validatedProperties.All(prop => prop.GetValue(request) == null))
            {
                result.Errors.Add(new ValidationFailure("", "A value for at least one of the fields must be provided."));
                return false;
            }

            return true;
        }
    }
}