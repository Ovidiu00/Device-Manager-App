using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.DataAcess.Filters.Factory_Method.Products
{
    public class DevicesTableFilter : ITableFilter<Device>
    {
        private readonly IUnitOfWork unitOfWork;

        public DevicesTableFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Device>> Filter(string column, string value)
        {

            return await unitOfWork.DeviceRepository.RawFind(column, value);
        }

        public async Task<IEnumerable<Device>> Filter(Dictionary<string, string> columnValuePairs)
        {
            return await unitOfWork.DeviceRepository.RawFind(columnValuePairs);
        }
    }
}
