using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Commands;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public partial class UpdateTemplateCommandValidator : AbstractValidator<UpdateTemplateCommand>
    {
        public UpdateTemplateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id cannot be null.");

            RuleFor(x => x.UpdateTemplate)
                .NotNull().WithMessage("Template cannot be null.");

            When(x => x.UpdateTemplate != null, () =>
            {
                RuleFor(x => x.UpdateTemplate.AnchorPoint)
                    .NotNull().WithMessage("AnchorPoint cannot be null.")
                    .SetValidator(new AnchorPointValidator());

                RuleFor(x => x.UpdateTemplate.Title)
                    .NotEmpty().WithMessage("Template Title cannot be empty.");

                RuleFor(x => x.UpdateTemplate.Attributes)
                    .NotNull().WithMessage("Template Attributes cannot be null.")
                    .Must(attrs => attrs != null && attrs.GroupBy(a => a.Id).Count() == attrs.Count)
                        .WithMessage("Attributes must be unique for the template");

                RuleForEach(x => x.UpdateTemplate.Attributes).ChildRules(attributes =>
                {
                    attributes.RuleFor(attr => attr.Name)
                        .NotEmpty().WithMessage("Attribute Name cannot be empty.");
                    attributes.RuleFor(attr => attr.Description)
                        .NotEmpty().WithMessage("Attribute Description cannot be empty.");
                });
            });
        }
    }
}
