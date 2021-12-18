using AutoMapper;
using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories;
using System.Collections.Generic;
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
        public async Task<Device>MapAddDeviceDTOtoDeviceEntity(AddDeviceDTO deviceToBeAdded)
        {
            var deviceEntity = await this.MapAddDeviceDTOtoDeviceEntity(deviceToBeAdded);

            var operatingSystemWithGivenName = await unitOfWork.OperatingSystemsRepository.FindSingle(x =>
                                                                                    x.Name == deviceToBeAdded.OperatingSystem
                                                                                 && x.Version == deviceToBeAdded.Version);
            if (operatingSystemWithGivenName != null)
                deviceEntity.OperatingSystemId = operatingSystemWithGivenName.Id;

            return deviceEntity;
        }

        public DeviceDTO MapDeviceEntityToDeviceDTO(Device deviceEntity)
        {
            return mapper.Map<DeviceDTO>(deviceEntity);
        }

        public IEnumerable<DeviceDTO> MapDeviceEntityToDeviceDTO(IEnumerable<Device> devices)
        {      
            foreach (Device device in devices)
                  yield return this.MapDeviceEntityToDeviceDTO(device);
        }

        public async Task<Device> MapEditDeviceDTOtoDeviceEntity(EditDeviceDTO deviceToBeEdited)
        {
            Device deviceToBeEditedEntity = mapper.Map<Device>(deviceToBeEdited);
          
            OperatingSystem operatingSystem = await unitOfWork.OperatingSystemsRepository.FindSingle(x =>
                                                                 x.Name == deviceToBeEdited.OperatingSystem &&
                                                                 x.Version == deviceToBeEdited.Version);

            deviceToBeEditedEntity.OperatingSystemId = operatingSystem?.Id;
            return deviceToBeEditedEntity;
        }
    }
}
