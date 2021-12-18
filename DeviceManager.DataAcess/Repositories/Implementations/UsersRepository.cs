using DeviceManager.DataAcess.EF.AppDBContext;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Repositories.Implementations
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        private readonly UserManager<User> userManager;

        public UsersRepository(DeviceManagerDBContext db,UserManager<User> userManager):base(db)
        {
            this.userManager = userManager;
        }

      
    }
}
