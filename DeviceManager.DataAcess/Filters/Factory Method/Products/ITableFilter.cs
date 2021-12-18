using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Filters.Factory_Method.Products
{
    public interface ITableFilter<T>
    {
        Task<IEnumerable<T>> Filter(Dictionary<string,string> columnValuePairs);
        Task<IEnumerable<T>> Filter(string column, string value);
    }
}
