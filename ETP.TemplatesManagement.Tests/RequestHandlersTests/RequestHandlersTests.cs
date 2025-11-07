using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.DTOs;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Queries;
using ETP.TemplatesManagement.ServiceHost.RequestHandlers;
using Moq;
using NUnit.Framework;

namespace ETP.TemplatesManagement.Tests.RequestHandlerTests
{
    [TestFixture]
    public class RequestHandlersTests
    {
        [Test]
        public async Task CreateTemplateHandler_MapsInputAndReturnsMappedTemplate()
        {
            var repoMock = new Mock<ITemplateRepository>();
            var mapperMock = new Mock<IMapper>();

            var mappedModel = new Data.Models.Template(); // model returned by mapper when mapping from TemplateBase
            var createdModel = new Data.Models.Template { Id = Guid.NewGuid() }; // model returned by repository
            var expectedDto = new SDK.DTOs.Template { Id = createdModel.Id }; // DTO expected to be returned

            mapperMock.Setup(m => m.Map<Data.Models.Template>(It.IsAny<TemplateBase>())).Returns(mappedModel);
            repoMock.Setup(r => r.CreateTemplate(mappedModel, It.IsAny<CancellationToken>())).ReturnsAsync(createdModel);
            mapperMock.Setup(m => m.Map<SDK.DTOs.Template>(createdModel)).Returns(expectedDto);

            var handler = new CreateTemplateHandler(repoMock.Object, mapperMock.Object);

            var request = new CreateTemplateCommand { Template = new TemplateBase { Title = "title" } };
            var result = await handler.Handle(request, CancellationToken.None);

            Assert.That(expectedDto == result);
            repoMock.Verify(r => r.CreateTemplate(mappedModel, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<Data.Models.Template>(request.Template), Times.Once);
            mapperMock.Verify(m => m.Map<SDK.DTOs.Template>(createdModel), Times.Once);
        }

        [Test]
        public async Task DeleteTemplateHandler_ForwardsRepositoryResult()
        {
            var repoMock = new Mock<ITemplateRepository>();
            var id = Guid.NewGuid();
            repoMock.Setup(r => r.DeleteTemplate(id, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var handler = new DeleteTemplateHandler(repoMock.Object);

            var result = await handler.Handle(new DeleteTemplateCommand { Id = id }, CancellationToken.None);

            Assert.That(result == false);
            repoMock.Verify(r => r.DeleteTemplate(id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetTemplateByAnchorPointHandler_MapsAnchorPointAndReturnsMappedTemplate()
        {
            var repoMock = new Mock<ITemplateRepository>();
            var mapperMock = new Mock<IMapper>();

            var sdkAnchor = new SDK.DTOs.AnchorPoint()
            {
                DeliveryOwner = new SDK.DTOs.DeliveryOwner(Guid.NewGuid(), "do"),
                ServiceLine = new SDK.DTOs.ServiceLine(Guid.NewGuid(), "sl"),
                MarketOffering = new SDK.DTOs.MarketOffering(Guid.NewGuid(), "mo")
            };
            var dataAnchor = new Data.Models.AnchorPoint();
            var dataTemplate = new Data.Models.Template { Id = Guid.NewGuid() };
            var expectedDto = new SDK.DTOs.Template { Id = dataTemplate.Id };

            mapperMock.Setup(m => m.Map<Data.Models.AnchorPoint>(It.IsAny<SDK.DTOs.AnchorPoint>())).Returns(dataAnchor);
            repoMock.Setup(r => r.GetTemplateByAnchorPoint(dataAnchor, It.IsAny<CancellationToken>())).ReturnsAsync(dataTemplate);
            mapperMock.Setup(m => m.Map<SDK.DTOs.Template>(dataTemplate)).Returns(expectedDto);

            var handler = new GetTemplateByAnchorPointHandler(repoMock.Object, mapperMock.Object);

            var query = new GetTemplateByAnchorPointQuery { AnchorPoint = sdkAnchor };
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.That(expectedDto == result);
            mapperMock.Verify(m => m.Map<Data.Models.AnchorPoint>(sdkAnchor), Times.Once);
            repoMock.Verify(r => r.GetTemplateByAnchorPoint(dataAnchor, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<SDK.DTOs.Template>(dataTemplate), Times.Once);
        }

        [Test]
        public async Task GetTemplateByIdHandler_UsesRepositoryAndMapsResult()
        {
            var repoMock = new Mock<ITemplateRepository>();
            var mapperMock = new Mock<IMapper>();

            var id = Guid.NewGuid();
            var dataTemplate = new Data.Models.Template { Id = id };
            var expectedDto = new SDK.DTOs.Template { Id = id };

            repoMock.Setup(r => r.GetTemplateById(id, It.IsAny<CancellationToken>())).ReturnsAsync(dataTemplate);
            mapperMock.Setup(m => m.Map<SDK.DTOs.Template>(dataTemplate)).Returns(expectedDto);

            var handler = new GetTemplateByIdHandler(repoMock.Object, mapperMock.Object);

            var result = await handler.Handle(new GetTemplateByIdQuery { Id = id }, CancellationToken.None);

            Assert.That(expectedDto == result);
            repoMock.Verify(r => r.GetTemplateById(id, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<SDK.DTOs.Template>(dataTemplate), Times.Once);
        }

        [Test]
        public async Task GetTemplatesHandler_MapsSearchOptionsAndReturnsMappedList()
        {
            var repoMock = new Mock<ITemplateRepository>();
            var mapperMock = new Mock<IMapper>();

            var sdkSearch = new SDK.DTOs.SearchOptions { Page = 1, Count = 10 };
            var dataSearch = new Data.Models.SearchOptions { Page = 1, Count = 10 };
            var dataTemplates = new List<Data.Models.Template> { new Data.Models.Template { Id = Guid.NewGuid() } };
            var expectedDtos = new List<SDK.DTOs.Template> { new SDK.DTOs.Template { Id = dataTemplates[0].Id } };

            mapperMock.Setup(m => m.Map<Data.Models.SearchOptions>(It.IsAny<SDK.DTOs.SearchOptions>())).Returns(dataSearch);
            repoMock.Setup(r => r.GetTemplates(dataSearch, It.IsAny<CancellationToken>())).ReturnsAsync(dataTemplates);
            mapperMock.Setup(m => m.Map<List<SDK.DTOs.Template>>(It.IsAny<object>())).Returns(expectedDtos);

            var handler = new GetTemplatesHandler(repoMock.Object, mapperMock.Object);

            var query = new GetTemplatesQuery { SearchObject = sdkSearch };
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.That(result != null);
            CollectionAssert.AreEqual(expectedDtos, result!);
            mapperMock.Verify(m => m.Map<Data.Models.SearchOptions>(sdkSearch), Times.Once);
            repoMock.Verify(r => r.GetTemplates(dataSearch, It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<List<SDK.DTOs.Template>>(It.IsAny<object>()), Times.AtLeastOnce);
        }

        [Test]
        public async Task UpdateTemplateHandler_SetsIdOnModelAndReturnsMappedTemplate()
        {
            var repoMock = new Mock<ITemplateRepository>();
            var mapperMock = new Mock<IMapper>();

            var id = Guid.NewGuid();
            var sdkTemplateBase = new TemplateBase { Title = "t" };
            var mappedModel = new Data.Models.Template(); // mapper returns this for mapping from TemplateBase
            var updatedDataModel = new Data.Models.Template { Id = id };
            var expectedDto = new SDK.DTOs.Template { Id = id };

            mapperMock.Setup(m => m.Map<Data.Models.Template>(sdkTemplateBase)).Returns(mappedModel);
            repoMock.Setup(r => r.UpdateTemplate(It.IsAny<Data.Models.Template>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedDataModel);
            mapperMock.Setup(m => m.Map<SDK.DTOs.Template>(updatedDataModel)).Returns(expectedDto);

            var handler = new UpdateTemplateHandler(repoMock.Object, mapperMock.Object);

            var request = new UpdateTemplateCommand { Id = id, UpdateTemplate = sdkTemplateBase };
            var result = await handler.Handle(request, CancellationToken.None);

            Assert.That(expectedDto == result);
            // verify that UpdateTemplate was called with a model whose Id equals request.Id
            repoMock.Verify(r => r.UpdateTemplate(It.Is<Data.Models.Template>(m => m.Id == id), It.IsAny<CancellationToken>()), Times.Once);
            mapperMock.Verify(m => m.Map<Data.Models.Template>(sdkTemplateBase), Times.Once);
            mapperMock.Verify(m => m.Map<SDK.DTOs.Template>(updatedDataModel), Times.Once);
        }
    }
}
