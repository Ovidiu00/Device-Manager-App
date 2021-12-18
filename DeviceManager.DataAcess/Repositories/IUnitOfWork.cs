using DeviceManager.DataAcess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();

        public IUsersRepository UsersRepository { get; }
        public IDeviceRepository DeviceRepository { get; }
        public IOperatingSystemsRepository OperatingSystemsRepository { get; }

    }
}
