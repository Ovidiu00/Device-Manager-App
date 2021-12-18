using DeviceManager.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Contracts.Dtos
{
    public class AddDeviceDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DeviceType Type { get; set; }
        public string OperatingSystem { get; set; }
        public int Version { get; set; }
        public string Processor { get; set; }
        public int RAMAmountInGB { get; set; }
    }
}
