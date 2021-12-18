using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Services.Mapping.DeviceMapper
{
    public interface IDeviceMapper
    {
        Task<Device> MapAddDeviceDTOtoDeviceEntity(AddDeviceDTO deviceToBeAdded);
        DeviceDTO MapDeviceEntityToDeviceDTO(Device deviceEntity);
    }
}
