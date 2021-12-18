using DeviceManager.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.EF.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DeviceType Type { get; set; }

        public int? OperatingSystemId { get; set; }
        public OperatingSystem OperatingSystem { get; set; }

        public string Processor { get; set; }
        public int RAMAmountInGB { get; set; }

        public User User { get; set; }
    }
}
