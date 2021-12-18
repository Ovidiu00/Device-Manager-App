using DeviceManager.Busniess.Commands.DevicesCommands;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.Busniess.Services.Mapping.DeviceMapper;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Commands_Handlers.Device_Commands_Handlers
{
    public class EditDeviceHandler : IRequestHandler<EditDeviceCommand, DeviceDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDeviceMapper deviceMapper;

        public EditDeviceHandler(IUnitOfWork unitOfWork, IDeviceMapper deviceMapper)
        {
            this.unitOfWork = unitOfWork;
            this.deviceMapper = deviceMapper;
        }
        public async Task<DeviceDTO> Handle(EditDeviceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingDevice = await unitOfWork.DeviceRepository.FindSingle(x => x.Id == request.id);

                if (existingDevice == null)
                    throw new DeviceNotFoundException($"Cant find device with id of {request.id}");

                Device modifiedDevice = await deviceMapper.MapEditDeviceDTOtoDeviceEntity(request.deviceToBeEdited);

                unitOfWork.DeviceRepository.UpdateIfModified(existingDevice, modifiedDevice, nameof(existingDevice.Id));

                await unitOfWork.Commit();

                return deviceMapper.MapDeviceEntityToDeviceDTO(modifiedDevice);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
       
}
