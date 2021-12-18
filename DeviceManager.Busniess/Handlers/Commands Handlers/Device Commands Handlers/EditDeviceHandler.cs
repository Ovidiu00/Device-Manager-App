using AutoMapper;
using DeviceManager.Busniess.Commands.DevicesCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.BaseException;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.Busniess.Handlers.Commands_Handlers.Device_Commands_Handlers
{
    public class EditDeviceHandler : IRequestHandler<EditDeviceCommand, DeviceDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EditDeviceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DeviceDTO> Handle(EditDeviceCommand request, CancellationToken cancellationToken)
        {
            var existingDevice = await unitOfWork.DeviceRepository.FindSingle(x => x.Id == request.id);

            if (existingDevice == null)
                throw new DeviceNotFoundException($"Cant find device with id of {request.id}");

            Device modifiedDevice = await MapEditDeviceDTOtoDeviceEntity(request.deviceToBeEdited);

            unitOfWork.DeviceRepository.UpdateIfModified(existingDevice, modifiedDevice, nameof(existingDevice.Id));

            await unitOfWork.Commit();

            return mapper.Map<DeviceDTO>(modifiedDevice);
        }

        internal async Task<Device> MapEditDeviceDTOtoDeviceEntity(EditDeviceDTO deviceToBeEdited)
        {
            Device deviceToBeEditedEntity = mapper.Map<Device>(deviceToBeEdited);
            deviceToBeEditedEntity.OperatingSystemId = await GetOperatingSystemIdFromDatabase(deviceToBeEdited.OperatingSystem, deviceToBeEdited.Version);
            return deviceToBeEditedEntity;
        }

        internal async Task<int?> GetOperatingSystemIdFromDatabase(string osName, int osVersion)
        {
            OperatingSystem operatingSystem = await unitOfWork.OperatingSystemsRepository.FindSingle(x =>
                                                                   x.Name == osName &&
                                                                   x.Version == osVersion);
            return operatingSystem?.Id;
        }
    }
}
