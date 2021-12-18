using DeviceManager.DataAcess.Filters.Factory_Method.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Filters.Factory_Method.Factories
{
    public interface TableFiltersFactory<T>
    {
        ITableFilter<T> CreateTableFilter();
    }
}
