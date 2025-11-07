using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Commands;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
    {
        public CreateTemplateCommandValidator()
        {
            RuleFor(x => x.Template)
                .NotNull().WithMessage("Template cannot be null.");

            When(x => x.Template != null, () =>
            {
                RuleFor(x => x.Template.AnchorPoint)
                .NotNull().WithMessage("AnchorPoint cannot be null.")
                .SetValidator(new AnchorPointValidator());

                RuleFor(x => x.Template.Title)
                    .NotEmpty().WithMessage("Template Title cannot be empty.");

                RuleFor(x => x.Template.Attributes)
                    .NotNull().WithMessage("Template Attributes cannot be null.")
                    .Must(attrs => attrs != null && attrs.GroupBy(a => a.Id).Count() == attrs.Count).WithMessage("Attributes must be unique for the template");

                RuleForEach(x => x.Template.Attributes).ChildRules(attributes =>
                {
                    attributes.RuleFor(attr => attr.Id)
                        .Must(id => id != Guid.Empty).WithMessage("Attribute Id cannot be empty.");
                    attributes.RuleFor(attr => attr.Name)
                        .NotEmpty().WithMessage("Attribute Name cannot be empty.");
                    attributes.RuleFor(attr => attr.Description)
                        .NotEmpty().WithMessage("Attribute Description cannot be empty.");
                });
            });
        }
    }
}
