using AutoMapper;
using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Services.Mapping.DeviceMapper
{
    public class DeviceMapper : IDeviceMapper
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeviceMapper(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Device> MapAddDeviceDTOtoDeviceEntity(AddDeviceDTO deviceToBeAdded)
        {
            var deviceEntity = mapper.Map<Device>(deviceToBeAdded);

            var operatingSystemWithGivenName = await unitOfWork.OperatingSystemsRepository.FindSingle(x =>
                                                                                    x.Name == deviceToBeAdded.Name
                                                                                 && x.Version == deviceToBeAdded.Version);
            if (operatingSystemWithGivenName != null)
                deviceEntity.OperatingSystemId = operatingSystemWithGivenName.Id;

            return deviceEntity;
        }

        public DeviceDTO MapDeviceEntityToDeviceDTO(Device deviceEntity)
        {
            return mapper.Map<DeviceDTO>(deviceEntity);
        }
    }
}
