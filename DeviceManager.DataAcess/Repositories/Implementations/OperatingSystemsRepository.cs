using DeviceManager.DataAcess.EF.AppDBContext;
using DeviceManager.DataAcess.Repositories.Interfaces;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.Repositories.Implementations
{
    public class OperatingSystemsRepository : BaseRepository<OperatingSystem>,IOperatingSystemsRepository
    {
        public OperatingSystemsRepository(DeviceManagerDBContext _db) : base(_db)
        {

        }
    }
}
