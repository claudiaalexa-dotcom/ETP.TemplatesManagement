using ETP.TemplatesManagement.SDK.DTOs;
using FluentValidation;

namespace ETP.TemplatesManagement.ServiceHost.Validators
{
    public class AnchorPointValidator : AbstractValidator<AnchorPoint>
    {
        public AnchorPointValidator()
        {
            RuleFor(x => x.DeliveryOwnerId)
                .Must(id => id != Guid.Empty).WithMessage("AnchorPoint Delivery Owner Id cannot be empty.");

            RuleFor(x => x.ServiceLineId)
                .Must(id => id != Guid.Empty).WithMessage("AnchorPoint Service Line Id cannot be empty.");

            RuleFor(x => x.MarketOfferingId)
                .Must(id => id != Guid.Empty).WithMessage("AnchorPoint Market Offering Id cannot be empty.");

            RuleFor(x => x.DeliveryOwnerName)
                .NotEmpty().WithMessage("AnchorPoint Delivery Owner Name cannot be empty.");

            RuleFor(x => x.ServiceLineName)
                .NotEmpty().WithMessage("AnchorPoint Service Line Name cannot be empty.");

            RuleFor(x => x.MarketOfferingName)
                .NotEmpty().WithMessage("AnchorPoint Market Offering Name cannot be empty.");
        }
    }
}
