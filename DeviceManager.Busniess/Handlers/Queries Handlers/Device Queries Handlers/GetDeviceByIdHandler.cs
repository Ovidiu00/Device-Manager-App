using AutoMapper;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Exceptions.DevicesExceptions;
using DeviceManager.Busniess.Queries.DevicesQueries;
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
        private readonly IMapper mapper;

        public GetDeviceByIdHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DeviceDTO> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await  unitOfWork.DeviceRepository.GetDevicesListWithUsersAndOperatingSystemAsync();

            var device = result.FirstOrDefault(x => x.Id == request.id);
            if(device == null) 
                throw new DeviceNotFoundException();
            

            return mapper.Map<DeviceDTO>(device);
        }
    }
}
