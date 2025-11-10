using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Queries;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class GetTemplatesByAnchorPointQueryValidator : AbstractValidator<GetTemplatesByAnchorPointQuery>
    {
        public GetTemplatesByAnchorPointQueryValidator()
        {
            RuleFor(x => x.SearchOptions)
                .NotNull().WithMessage("AnchorPointSearchOptions cannot be null.");
        }
    }
}
