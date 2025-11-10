using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Validators;
using NUnit.Framework;

namespace ETP.TemplatesManagement.Tests.ValidationTests
{
    [TestFixture]
    public class AnchorPointValidatorTests
    {
        private AnchorPointValidator _validator = null!;

        [SetUp]
        public void SetUp() => _validator = new AnchorPointValidator();

        private AnchorPoint CreateValid() =>
            new AnchorPoint
            {
                DeliveryOwnerId = Guid.NewGuid(),
                DeliveryOwnerName = "do",
                ServiceLineId = Guid.NewGuid(),
                ServiceLineName = "sl",
                MarketOfferingId = Guid.NewGuid(),
                MarketOfferingName = "mo"
            };

        [Test]
        public void ValidAnchorPoint_Passes()
        {
            var result = _validator.Validate(CreateValid());
            Assert.That(result.IsValid);
        }

        [Test]
        public void EmptyDeliveryOwnerId_FailsWithExpectedMessage()
        {
            var anchorPoint = CreateValid();
            anchorPoint = anchorPoint with { DeliveryOwnerId = Guid.Empty };

            var result = _validator.Validate(anchorPoint);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Delivery Owner Id cannot be empty."));
        }

        [Test]
        public void EmptyServiceLineId_FailsWithExpectedMessage()
        {
            var anchorPoint = CreateValid();
            anchorPoint = anchorPoint with { ServiceLineId = Guid.Empty };

            var result = _validator.Validate(anchorPoint);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Service Line Id cannot be empty."));
        }

        [Test]
        public void EmptyMarketOfferingId_FailsWithExpectedMessage()
        {
            var anchorPoint = CreateValid();
            anchorPoint = anchorPoint with { MarketOfferingId = Guid.Empty };

            var result = _validator.Validate(anchorPoint);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Market Offering Id cannot be empty."));
        }

        [Test]
        public void EmptyDeliveryOwnerName_FailsWithExpectedMessage()
        {
            var anchorPoint = CreateValid();
            anchorPoint = anchorPoint with { DeliveryOwnerName = string.Empty };

            var result = _validator.Validate(anchorPoint);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Delivery Owner Name cannot be empty."));
        }

        [Test]
        public void EmptyServiceLineName_FailsWithExpectedMessage()
        {
            var anchorPoint = CreateValid();
            anchorPoint = anchorPoint with { ServiceLineName = string.Empty };

            var result = _validator.Validate(anchorPoint);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Service Line Name cannot be empty."));
        }

        [Test]
        public void EmptyMarketOfferingName_FailsWithExpectedMessage()
        {
            var anchorPoint = CreateValid();
            anchorPoint = anchorPoint with { MarketOfferingName = string.Empty };

            var result = _validator.Validate(anchorPoint);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Market Offering Name cannot be empty."));
        }
    }
}
