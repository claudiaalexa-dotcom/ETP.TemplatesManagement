using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Queries;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class GetTemplatesQueryValidator : AbstractValidator<GetTemplatesQuery>
    {
        public GetTemplatesQueryValidator()
        {
            RuleFor(x => x.SearchObject)
                .NotNull().WithMessage("Search object cannot be null.");
        }
    }
}
