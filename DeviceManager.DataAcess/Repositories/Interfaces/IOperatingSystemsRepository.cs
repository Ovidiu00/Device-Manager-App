using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.Repositories.Interfaces
{
    public interface IOperatingSystemsRepository : IBaseRepository<OperatingSystem>
    {
    }
}
