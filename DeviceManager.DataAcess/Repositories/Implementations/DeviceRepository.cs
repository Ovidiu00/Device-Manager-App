using DeviceManager.DataAcess.EF.AppDBContext;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.Repositories.Implementations
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {

        public DeviceRepository(DeviceManagerDBContext _db) : base(_db)
        {

        }

        public async Task<Device> GetDevice(int id)
        {
            return await _db.Devices.Include(x => x.User).Include(X => X.OperatingSystem).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            return await GetDevicesFromDB()
                .ToListAsync();
        }

        private IQueryable<Device> GetDevicesFromDB(int page = 0, int itemsPerPage = 0, Expression<System.Func<Device, bool>> ex = null)
        {
            var devices = _db
                .Devices
                .Include(x => x.User)
                .Include(x => x.OperatingSystem)
                .AsNoTracking();

            if (ex != null)
            {
               devices = devices.Where(ex);
            }
            if (itemsPerPage != 0 || page != 0)
            {
               devices = devices.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            }

            var foo = devices.ToList();
            return devices;
        }

        public async Task<IEnumerable<Device>> GetDevicesPaginated(int page, int itemsPerPage, Expression<System.Func<Device, bool>> ex = null)
        {

            return await GetDevicesFromDB(page,itemsPerPage,ex)
                .ToListAsync();
        }

        public OperatingSystem GetOperatingSystemByNameAsync(string Name)
        {
            return _db.OperatingSystems.FirstOrDefault(x => x.Name == Name);
        }
    }
}
