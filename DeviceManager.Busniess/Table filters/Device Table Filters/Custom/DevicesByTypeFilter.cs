using DeviceManager.Busniess.ExtensionMethods;
using DeviceManager.Contracts.Enums;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Filters.Table_filters;
using System;
using System.Linq.Expressions;

namespace DeviceManager.Busniess.Table_filters.Device_Table_Filters.Custom
{
    public class DevicesByTypeFilter : IDeviceTableFilter
    {
        public Expression<Func<Device, bool>> Filter(string value)
        {
            return x => x.Type == value.ConvertToDeviceType();
        }
    }
}
