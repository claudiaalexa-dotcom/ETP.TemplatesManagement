using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Validators;
using NUnit.Framework;

namespace ETP.TemplatesManagement.Tests.ValidationTests
{
    [TestFixture]
    public class UpdateTemplateCommandValidatorTests
    {
        private UpdateTemplateCommandValidator _validator = null!;

        [SetUp]
        public void SetUp() => _validator = new UpdateTemplateCommandValidator();

        private TemplateBase CreateValidTemplate()
        {
            return new TemplateBase
            {
                AnchorPoint = new AnchorPoint
                {
                    DeliveryOwnerId = Guid.NewGuid(),
                    DeliveryOwnerName = "do",
                    ServiceLineId = Guid.NewGuid(),
                    ServiceLineName = "sl",
                    MarketOfferingId = Guid.NewGuid(),
                    MarketOfferingName = "mo"
                },
                Title = "Valid Title",
                Attributes = new List<SDK.DTOs.Attribute>
                {
                    new SDK.DTOs.Attribute { Id = Guid.NewGuid(), Name = "A1", Description = "D1"}
                }
            };
        }

        [Test]
        public void ValidUpdateCommand_Passes()
        {
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = CreateValidTemplate() };
            var result = _validator.Validate(cmd);
            Assert.That(result.IsValid);
        }

        [Test]
        public void NullUpdateTemplate_FailsWithExpectedMessage()
        {
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = null! };
            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("Template cannot be null."));
        }

        [Test]
        public void NullAnchorPoint_FailsWithExpectedMessage()
        {
            var t = CreateValidTemplate();
            t.AnchorPoint = null!;
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = t };

            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("AnchorPoint cannot be null."));
        }

        [Test]
        public void EmptyTitle_FailsWithExpectedMessage()
        {
            var t = CreateValidTemplate();
            t.Title = string.Empty;
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = t };

            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("Template Title cannot be empty."));
        }

        [Test]
        public void NullAttributes_FailsWithExpectedMessage()
        {
            var t = CreateValidTemplate();
            t.Attributes = null!;
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = t };

            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("Template Attributes cannot be null."));
        }

        [Test]
        public void DuplicateAttributeIds_FailsWithExpectedMessage()
        {
            var id = Guid.NewGuid();
            var t = CreateValidTemplate();
            t.Attributes = new List<SDK.DTOs.Attribute>
            {
                new SDK.DTOs.Attribute { Id = id, Name = "A1", Description = "D1" },
                new SDK.DTOs.Attribute { Id = id, Name = "A2", Description = "D2" }
            };
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = t };

            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("Attributes must be unique for the template"));
        }

        [Test]
        public void AttributeMissingName_FailsWithExpectedMessage()
        {
            var t = CreateValidTemplate();
            t.Attributes = new List<SDK.DTOs.Attribute>
            {
                new SDK.DTOs.Attribute { Id = Guid.NewGuid(), Name = string.Empty, Description = "D1" }
            };
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = t };

            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("Attribute Name cannot be empty."));
        }

        [Test]
        public void AttributeMissingDescription_FailsWithExpectedMessage()
        {
            var t = CreateValidTemplate();
            t.Attributes = new List<SDK.DTOs.Attribute>
            {
                new SDK.DTOs.Attribute { Id = Guid.NewGuid(), Name = "A1", Description = string.Empty }
            };
            var cmd = new UpdateTemplateCommand { Id = Guid.NewGuid(), UpdateTemplate = t };

            var result = _validator.Validate(cmd);
            Assert.That(!result.IsValid);
            Assert.That(result.Errors.Select(e => e.ErrorMessage), Has.Member("Attribute Description cannot be empty."));
        }
    }
}
