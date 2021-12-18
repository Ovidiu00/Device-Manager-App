using DeviceManager.DataAcess.EF.AppDBContext;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories.Implementations;
using DeviceManager.DataAcess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeviceManagerDBContext db;

        private IUsersRepository userRepository;
        private IDeviceRepository deviceRepository;
        private IOperatingSystemsRepository operatingSystemsRepository;
        public UnitOfWork(DeviceManagerDBContext db)
        {
            this.db = db;
        }


        public IUsersRepository UsersRepository
        {
            get
            {
                 IUserStore<User> _store = new UserStore<User>(db);

                return userRepository = userRepository ?? new UsersRepository(db,new UserManager<User>(_store, null,null,null,null,null,null,null,null));
            }
        }
        public IDeviceRepository DeviceRepository
        {
            get
            {
                return deviceRepository = deviceRepository ?? new DeviceRepository(db);
            }
        }

        public IOperatingSystemsRepository OperatingSystemsRepository
        {
            get
            {
                return operatingSystemsRepository = operatingSystemsRepository ?? new OperatingSystemsRepository(db);
            }
        }

        public async Task Commit()
        {
            await db.SaveChangesAsync();
        }
    }
}
