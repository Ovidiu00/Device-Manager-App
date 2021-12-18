using AutoMapper;
using DeviceManager.API;
using DeviceManager.API.ViewModels;
using DeviceManager.Busniess.Commands.DevicesCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.BaseException;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.Busniess.Queries.DevicesQueries;
using DeviceManager.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.API_UnitTests
{
    public class DeviceControllerUnitTests
    {
        private readonly Mock<IMediator> mockMediator;
        private IMapper mapper;
        private Mock<ILogger<DevicesController>> loggerMock;
        public DeviceControllerUnitTests()
        {
            mockMediator = new Mock<IMediator>();
            loggerMock = new Mock<ILogger<DevicesController>>();

            var myProfile = new AutoMapperApiProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            mapper = new Mapper(configuration);

        }
        [Fact]
        public async Task Device_ActionExecutes_ReturnsNotNulLValue()
        {
            // Arrange
            mockMediator.Setup(repo => repo.Send(It.IsAny<GetDeviceByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeviceDTO() { Name = "boo" });

            var controller = new DevicesController(loggerMock.Object, mockMediator.Object, mapper);

            //// Act
            var result = (OkObjectResult)(await controller.Device(1));
            var resultModel = (DeviceVM)result.Value;

            //// Assert

            Assert.IsType<DeviceVM>(resultModel);
            Assert.NotNull(resultModel);


        }

        [Fact]
        public async Task Devices_ActionExecutes_ReturnsListOfDeviceVM()
        {
            // Arrange
            mockMediator.Setup(repo => repo.Send(It.IsAny<GetDevicesListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetTestDevices());

            var controller = new DevicesController(loggerMock.Object, mockMediator.Object, mapper);

            //// Act
            var result = (OkObjectResult)(await controller.Devices());
            //// Assert
            var resultModel = (IEnumerable<DeviceVM>)result.Value;
            Assert.IsAssignableFrom<IEnumerable<DeviceVM>>(resultModel);
            Assert.Equal(2, resultModel.Count());
        }
        [Fact]
        public async Task CreateDevice_ValidModel_ReturnsCreatedDevice()
        {
            // Arrange

            var addDeviceDTO = new AddDeviceVM()
            {
                Name = "Bluboo",
                Processor = "FOO"
            };

            var returnDeviceAfterCreating = new DeviceDTO()
            {
                Name = "Bluboo",
                Processor = "FOO"
            };



            var mockMediatr = new Mock<IMediator>();
            mockMediatr.Setup(repo => repo.Send(It.IsAny<AddDeviceCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(returnDeviceAfterCreating);

            var controller = new DevicesController(loggerMock.Object, mockMediatr.Object, mapper);

            //// Act
            var result = (OkObjectResult)(await controller.AddDevice(addDeviceDTO));

            //// Assert
            var resultModel = (DeviceVM)result.Value;

            Assert.Equal("Bluboo", resultModel.Name);
            Assert.Equal("FOO", resultModel.Processor);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreateDevice_CreateFailsBecauseDeviceAlreadyExists_Return409StatusCode()
        {
            // Arrange

            var mockMediatr = new Mock<IMediator>();
            mockMediatr.Setup(repo => repo.Send(It.IsAny<AddDeviceCommand>(), It.IsAny<CancellationToken>())).Throws(new DeviceAlreadyExistsException());


            var controller = new DevicesController(loggerMock.Object, mockMediatr.Object, mapper);

            //// Act 

            var result = (ObjectResult)(await controller.AddDevice(new AddDeviceVM()));

            //// Assert

            Assert.Equal(409, result.StatusCode);
        }
        //TODO: https://adamstorr.azurewebsites.net/blog/mocking-ilogger-with-moq
        //   [Fact]
        //   public async Task CreateDevice_CreateFailsBecauseDeviceAlreadyExists_LoggerShouldNotBeCalled()
        //   {
        //       // Arrange

        //       var mockMediatr = new Mock<IMediator>();
        //       mockMediatr.Setup(repo => repo.Send(It.IsAny<AddDeviceCommand>(), It.IsAny<CancellationToken>())).Throws(new DeviceAlreadyExistsException());


        //       var controller = new DevicesController(loggerMock.Object, mockMediatr.Object, mapper);

        //       //// Act 

        //       var result = (ObjectResult)(await controller.AddDevice(new AddDeviceVM()));

        //       //// Assert

        //       loggerMock.Verify(logger => logger.Log(
        //    It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
        //    It.Is<EventId>(eventId => eventId.Id == 0),
        //    It.Is<It.IsAnyType>((@object, @type) => It.IsAny<string>() && @type.Name == "FormattedLogValues"),
        //    It.IsAny<Exception>(),
        //    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        //Times.Once);
        //   }
        [Fact]
        public async Task CreateDevice_CreateFailsBecauseApplicationError_Return500StatusCode()
        {
            // Arrange

            var mockMediatr = new Mock<IMediator>();
            mockMediatr.Setup(repo => repo.Send(It.IsAny<AddDeviceCommand>(), It.IsAny<CancellationToken>())).Throws(new AplicationBaseException());


            var controller = new DevicesController(loggerMock.Object, mockMediatr.Object, mapper);

            //// Act 

            var result = (ObjectResult)(await controller.AddDevice(new AddDeviceVM()));

            //// Assert

            Assert.Equal(500, result.StatusCode);
        }
        private IEnumerable<DeviceDTO> GetTestDevices()
        {
            var testList = new List<DeviceDTO>();

            testList.Add(new DeviceDTO()
            {
                Name = "FOO",
                Manufacturer = "blabla",
                OperatingSystemId = 1,
                OperatingSystem = new DeviceManager.DataAcess.EF.Entities.OperatingSystem() { Id = 1, Name = "WINDOWS", Version = 10 },
                RAMAmountInGB = 15,
                Type = DeviceManager.Contracts.Enums.DeviceType.PHONE
            });
            testList.Add(new DeviceDTO()
            {
                Name = "af",
                Manufacturer = "ffsd",
                OperatingSystemId = 1,
                OperatingSystem = new DeviceManager.DataAcess.EF.Entities.OperatingSystem() { Id = 1, Name = "WINDOWS", Version = 10 },
                RAMAmountInGB = 22,
                Type = DeviceManager.Contracts.Enums.DeviceType.TABLET
            });


            return testList;
        }
    }
}
