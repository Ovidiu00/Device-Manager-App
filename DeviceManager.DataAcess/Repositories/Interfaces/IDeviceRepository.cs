using DeviceManager.DataAcess.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.Repositories.Interfaces
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Task<IEnumerable<Device>> GetDevices();
        Task<IEnumerable<Device>> GetDevicesPaginated(int page,int itemsPerPage, Expression<Func<Device, bool>> ex = null);

        Task<Device> GetDevice(int id);
        OperatingSystem GetOperatingSystemByNameAsync(string Name);
    }
}
