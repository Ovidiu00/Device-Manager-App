using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Filters.Factory_Method.Products;
using DeviceManager.DataAcess.Repositories;
using System;

namespace DeviceManager.DataAcess.Filters.Factory_Method.Factories
{
    public class DevicesTableFilterFactory : TableFiltersFactory<Device>
    {
        private readonly IUnitOfWork unitOfWork;

        public DevicesTableFilterFactory(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    
        public ITableFilter<Device> CreateTableFilter()
        {
            return new DevicesTableFilter(unitOfWork);
        }
    }
}
