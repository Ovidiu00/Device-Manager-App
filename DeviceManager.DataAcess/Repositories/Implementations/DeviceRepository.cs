using DeviceManager.DataAcess.EF.AppDBContext;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.Repositories.Implementations
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {

        public DeviceRepository(DeviceManagerDBContext _db) :base(_db)
        {

        }

        public async Task<IEnumerable<Device>> GetDevicesListWithUsersAndOperatingSystemAsync()
        {
           var devicesWithCorespondingUsers = _db.Devices.Include(x => x.User).Include(x=>x.OperatingSystem).AsNoTracking();

            return await devicesWithCorespondingUsers.ToListAsync();

        }

        public OperatingSystem GetOperatingSystemByNameAsync(string Name)
        {
            return _db.OperatingSystems.FirstOrDefault(x => x.Name == Name);
        }
    }
}
