using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Commands;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class DeleteTemplateCommandValidator : AbstractValidator<DeleteTemplateCommand>
    {
        public DeleteTemplateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id cannot be null.");
        }
    }
}
