using DeviceManager.DataAcess.Filters.Table_filters;

namespace DeviceManager.DataAcess.Filters.Factories
{
    public interface IDeviceTableFilterFactory
    {
        IDeviceTableFilter CreateTableFilter(string selector);
    }
}
