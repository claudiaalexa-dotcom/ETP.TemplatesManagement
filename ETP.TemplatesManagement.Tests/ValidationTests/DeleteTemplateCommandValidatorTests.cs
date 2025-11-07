using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Validators;
using NUnit.Framework;

namespace ETP.TemplatesManagement.Tests.ValidationTests
{

    [TestFixture]
    public class DeleteTemplateCommandValidatorTests
    {
        private DeleteTemplateCommandValidator _validator = null!;

        [SetUp]
        public void SetUp() => _validator = new DeleteTemplateCommandValidator();

        [Test]
        public void ValidDeleteCommand_Passes()
        {
            var cmd = new DeleteTemplateCommand { Id = Guid.NewGuid() };
            var result = _validator.Validate(cmd);
            Assert.That(result.IsValid);
        }
    }
}
