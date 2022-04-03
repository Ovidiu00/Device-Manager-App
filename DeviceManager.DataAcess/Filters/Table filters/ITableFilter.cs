using System;
using System.Linq.Expressions;

namespace DeviceManager.DataAcess.Filters.Table_filters
{
    public interface ITableFilter<T>
    {
        Expression<Func<T, bool>> Filter(string value);
    }
}
