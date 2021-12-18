using AutoMapper;
using DeviceManager.Busniess.AutoMapperBusiness;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Queries.DevicesQueries;
using DeviceManager.DataAcess.EF.Entities;
using DeviceManager.DataAcess.Filters.Factory_Method.Factories;
using DeviceManager.DataAcess.Filters.Factory_Method.Products;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handlers.Queries_Handlers.Device_Queries_Handlers
{
    public class GetDevicesListHandler : IRequestHandler<GetDevicesListQuery, IEnumerable<DeviceDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetDevicesListHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<DeviceDTO>> Handle(GetDevicesListQuery request, CancellationToken cancellationToken)
        {
            TableFiltersFactory<Device> devicesTableFilterFactory = new DevicesTableFilterFactory(unitOfWork);

            ITableFilter<Device> filter = devicesTableFilterFactory.CreateTableFilter();


            var filterPairs = new Dictionary<string, string>() {
                {"Processor","PROCESOR-NAME" },
                {"OperatingSystemId","5" }
            };
            var results = await filter.Filter(filterPairs);
            

            var totalDevices = await unitOfWork.DeviceRepository.GetDevicesListWithUsersAndOperatingSystemAsync();

            return mapper.Map<IEnumerable<DeviceDTO>>(totalDevices);
        }
    }
}
