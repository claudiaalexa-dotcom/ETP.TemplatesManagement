using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Queries;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class GetTemplateByAnchorPointQueryValidator : AbstractValidator<GetTemplateByAnchorPointQuery>
    {
        public GetTemplateByAnchorPointQueryValidator()
        {
            RuleFor(x => x.AnchorPoint)
                .NotNull().WithMessage("AnchorPoint cannot be null.")
                .SetValidator(new AnchorPointValidator());
        }
    }
}
