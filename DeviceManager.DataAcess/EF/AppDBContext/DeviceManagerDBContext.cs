using DeviceManager.Contracts.Enums;
using DeviceManager.DataAcess.EF.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OperatingSystem = DeviceManager.DataAcess.EF.Entities.OperatingSystem;

namespace DeviceManager.DataAcess.EF.AppDBContext
{
    public class DeviceManagerDBContext : IdentityDbContext
    {
        public DeviceManagerDBContext(DbContextOptions<DeviceManagerDBContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OperatingSystem>().HasData(
                new OperatingSystem { Id = 1, Name ="Windows",Version=10 },
                new OperatingSystem { Id = 2, Name = "macOS", Version = 2 },
                new OperatingSystem { Id = 3, Name = "Linux", Version = 5 },
                new OperatingSystem { Id = 4, Name = "TempleOS", Version = 33 },
                new OperatingSystem { Id = 5, Name = "Android", Version = 2 }
                );

            modelBuilder.Entity<Device>().HasData(
              new Device { Id = 1, Name = "RFL-200", Manufacturer= "SAMSUNG ",OperatingSystemId=5,Type=DeviceType.PHONE,RAMAmountInGB=3,Processor="PROCESOR-NAME"},
              new Device { Id = 2, Name = "IQOO 7 LEGEND.", Manufacturer = "SAMSUNG ", OperatingSystemId = 2, Type = DeviceType.PHONE, RAMAmountInGB = 31, Processor = "PROCESOR-NAME" },
              new Device { Id = 3, Name = "VIVO X70 PRO+", Manufacturer = "VOLVO ", OperatingSystemId = 1, Type = DeviceType.TABLET, RAMAmountInGB = 32, Processor = "PROCESOR-NAME" },
              new Device { Id = 4, Name = "GALAXY NOTE 20 ULTRA.", Manufacturer = "Microsoft ", OperatingSystemId = 3, Type = DeviceType.TABLET, RAMAmountInGB = 43, Processor = "PROCESOR-NAME" }
              );

         
           

        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<OperatingSystem> OperatingSystems { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
