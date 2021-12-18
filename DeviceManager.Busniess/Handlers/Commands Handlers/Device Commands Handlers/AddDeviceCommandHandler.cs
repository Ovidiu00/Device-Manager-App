using AutoMapper;
using DeviceManager.Busniess.Commands.DevicesCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.Busniess.Services.Mapping.DeviceMapper;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Commands_Handlers.Device_Commands_Handlers
{
    public class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, DeviceDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDeviceMapper deviceMapper;

        public AddDeviceCommandHandler(IUnitOfWork unitOfWork, IDeviceMapper deviceMapper)
        {
            this.unitOfWork = unitOfWork;
            this.deviceMapper = deviceMapper;
        }
        public async Task<DeviceDTO> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingDeviceWithGivenName = await unitOfWork.DeviceRepository.FindSingle(x => x.Name == request.deviceToBeAdded.Name);
                if (existingDeviceWithGivenName != null)
                    throw new DeviceAlreadyExistsException($"There is already a device named {request.deviceToBeAdded.Name}");

                Device deviceEntity = await deviceMapper.MapAddDeviceDTOtoDeviceEntity(request.deviceToBeAdded);

                unitOfWork.DeviceRepository.Add(deviceEntity);
                await unitOfWork.Commit();

                //TO DO  - EXTENSION METHOD!
                return deviceMapper.MapDeviceEntityToDeviceDTO(deviceEntity);
            }
            catch (Exception E)
            {
                throw;
            }
        }
    }
}
