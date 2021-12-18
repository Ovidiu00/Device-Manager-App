using DeviceManager.Contracts.Enums;

namespace DeviceManager.Busniess.Dtos
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
