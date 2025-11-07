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
                DeliveryOwner = new DeliveryOwner(Guid.NewGuid(), "DO"),
                ServiceLine = new ServiceLine(Guid.NewGuid(), "SL"),
                MarketOffering = new MarketOffering(Guid.NewGuid(), "MO")
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
            var ap = CreateValid();
            ap = ap with { DeliveryOwner = new DeliveryOwner(Guid.Empty, ap.DeliveryOwner.Name) };

            var result = _validator.Validate(ap);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Delivery Owner Id cannot be empty."));
        }

        [Test]
        public void EmptyServiceLineId_FailsWithExpectedMessage()
        {
            var ap = CreateValid();
            ap = ap with { ServiceLine = new ServiceLine(Guid.Empty, ap.ServiceLine.Name) };

            var result = _validator.Validate(ap);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Service Line Id cannot be empty."));
        }

        [Test]
        public void EmptyMarketOfferingId_FailsWithExpectedMessage()
        {
            var ap = CreateValid();
            ap = ap with { MarketOffering = new MarketOffering(Guid.Empty, ap.MarketOffering.Name) };

            var result = _validator.Validate(ap);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Market Offering Id cannot be empty."));
        }

        [Test]
        public void EmptyDeliveryOwnerName_FailsWithExpectedMessage()
        {
            var ap = CreateValid();
            ap = ap with { DeliveryOwner = new DeliveryOwner(ap.DeliveryOwner.Id, string.Empty) };

            var result = _validator.Validate(ap);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Delivery Owner Name cannot be empty."));
        }

        [Test]
        public void EmptyServiceLineName_FailsWithExpectedMessage()
        {
            var ap = CreateValid();
            ap = ap with { ServiceLine = new ServiceLine(ap.ServiceLine.Id, string.Empty) };

            var result = _validator.Validate(ap);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Service Line Name cannot be empty."));
        }

        [Test]
        public void EmptyMarketOfferingName_FailsWithExpectedMessage()
        {
            var ap = CreateValid();
            ap = ap with { MarketOffering = new MarketOffering(ap.MarketOffering.Id, string.Empty) };

            var result = _validator.Validate(ap);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint Market Offering Name cannot be empty."));
        }
    }
}
