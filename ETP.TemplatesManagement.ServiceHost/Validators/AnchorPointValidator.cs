using ETP.TemplatesManagement.SDK.DTOs;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class AnchorPointValidator : AbstractValidator<AnchorPoint>
    {
        public AnchorPointValidator()
        {
            RuleFor(x => x.DeliveryOwner.Id)
                .Must(id => id != Guid.Empty).WithMessage("AnchorPoint Delivery Owner Id cannot be empty.");

            RuleFor(x => x.ServiceLine.Id)
                .Must(id => id != Guid.Empty).WithMessage("AnchorPoint Service Line Id cannot be empty.");

            RuleFor(x => x.MarketOffering.Id)
                .Must(id => id != Guid.Empty).WithMessage("AnchorPoint Market Offering Id cannot be empty.");

            RuleFor(x => x.DeliveryOwner.Name)
                .NotEmpty().WithMessage("AnchorPoint Delivery Owner Name cannot be empty.");

            RuleFor(x => x.ServiceLine.Name)
                .NotEmpty().WithMessage("AnchorPoint Service Line Name cannot be empty.");

            RuleFor(x => x.MarketOffering.Name)
                .NotEmpty().WithMessage("AnchorPoint Market Offering Name cannot be empty.");
        }
    }
}
