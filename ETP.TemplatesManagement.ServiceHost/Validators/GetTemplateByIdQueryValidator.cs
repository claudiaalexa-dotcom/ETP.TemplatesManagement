using FluentValidation;
using ETP.TemplatesManagement.ServiceHost.Queries;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class GetTemplateByIdQueryValidator : AbstractValidator<GetTemplateByIdQuery>
    {
        public GetTemplateByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id cannot be null.");
        }
    }
}
