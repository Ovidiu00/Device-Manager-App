using AutoMapper;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.Busniess.Queries.DevicesQueries;
using DeviceManager.Busniess.Services.Mapping.DeviceMapper;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Queries_Handlers.Device_Queries_Handlers
{
    public class GetDeviceByIdHandler : IRequestHandler<GetDeviceByIdQuery, DeviceDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDeviceMapper deviceMapper;

        public GetDeviceByIdHandler(IUnitOfWork unitOfWork,IDeviceMapper deviceMapper)
        {
            this.unitOfWork = unitOfWork;
            this.deviceMapper = deviceMapper;
        }
        public async Task<DeviceDTO> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await unitOfWork.DeviceRepository.GetDevicesListWithUsersAndOperatingSystemAsync();

                var device = result.FirstOrDefault(x => x.Id == request.id);
                if (device == null)
                    throw new DeviceNotFoundException();


                return deviceMapper.MapDeviceEntityToDeviceDTO(device);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
