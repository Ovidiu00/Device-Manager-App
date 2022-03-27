using DeviceManager.Busniess.Table_filters.Device_Table_Filters.Custom;
using DeviceManager.DataAcess.Filters.Factories;
using DeviceManager.DataAcess.Filters.Table_filters;
using System;

namespace DeviceManager.Busniess.Table_filters.Factories
{
    public class DeviceTableFilterFactory : IDeviceTableFilterFactory
    {
        IDeviceTableFilter IDeviceTableFilterFactory.CreateTableFilter(string selector)
        {
            if (string.IsNullOrEmpty(selector))
            {
                throw new System.ArgumentException($"'{nameof(selector)}' cannot be null or empty.", nameof(selector));
            }

            switch (selector.ToUpper())
            {
                case "OS": return new DevicesByOperatingSystemFilter();
                case "TYPE": return new DevicesByTypeFilter();


                default: throw new NotImplementedException();
            }
        }
    }
}
