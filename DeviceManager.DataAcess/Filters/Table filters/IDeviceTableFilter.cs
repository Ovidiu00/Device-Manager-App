using DeviceManager.DataAcess.EF.Entities;
using System;
using System.Linq.Expressions;

namespace DeviceManager.DataAcess.Filters.Table_filters
{
    public interface IDeviceTableFilter
    {
        Expression<Func<Device, bool>> Filter(string value);
    }
}
