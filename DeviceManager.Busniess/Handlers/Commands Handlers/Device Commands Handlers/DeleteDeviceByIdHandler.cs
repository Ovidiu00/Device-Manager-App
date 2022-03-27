using DeviceManager.Busniess.Commands.DevicesCommands;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Commands_Handlers.Device_Commands_Handlers
{
    public class DeleteDeviceByIdHandler : IRequestHandler<DeleteDeviceByIdCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteDeviceByIdHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteDeviceByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deviceWithGivenID = await unitOfWork.DeviceRepository.FindSingle(x => x.Id == request.id);

                if (deviceWithGivenID == null)
                    throw new DeviceNotFoundException($"Can't find device with id of {request.id}");


                unitOfWork.DeviceRepository.Delete(deviceWithGivenID);
                await unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
