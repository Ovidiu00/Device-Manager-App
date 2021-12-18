namespace DeviceManager.API.ViewModels
{
    public class DeviceVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Type { get; set; }
        public string OperatingSystem { get; set; }
        public int OS_Version { get; set; }
        public string Processor { get; set; }
        public int RAMAmountInGB { get; set; }

        public string UserId { get; set; }
        public string UsersName { get; set; }
    }
}
