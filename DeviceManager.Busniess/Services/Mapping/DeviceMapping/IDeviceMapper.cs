using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Services.Mapping.DeviceMapper
{
    public interface IDeviceMapper
    {
        Task<Device> MapAddDeviceDTOtoDeviceEntity(AddDeviceDTO deviceToBeAdded);
        IEnumerable<DeviceDTO> MapDeviceEntityToDeviceDTO(IEnumerable<Device> devices);
        DeviceDTO MapDeviceEntityToDeviceDTO(Device deviceEntity);
        Task<Device> MapEditDeviceDTOtoDeviceEntity(EditDeviceDTO deviceToBeEdited);
    }
}
