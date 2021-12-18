using DeviceManager.DataAcess.EF.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.Repositories.Interfaces
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Task<IEnumerable<Device>> GetDevicesListWithUsersAndOperatingSystemAsync();
        OperatingSystem GetOperatingSystemByNameAsync(string Name);
    }
}
