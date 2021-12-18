using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Queries.DevicesQueries;
using DeviceManager.Busniess.Services.Mapping.DeviceMapper;
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
        private readonly IDeviceMapper deviceMapper;
        public GetDevicesListHandler(IUnitOfWork unitOfWork, IDeviceMapper deviceMapper)
        {
            this.unitOfWork = unitOfWork;
            this.deviceMapper = deviceMapper;
        }
        public async Task<IEnumerable<DeviceDTO>> Handle(GetDevicesListQuery request, CancellationToken cancellationToken)
        {
            //TODO: CREATE FILTER ENDPOINT
            //TableFiltersFactory<Device> devicesTableFilterFactory = new DevicesTableFilterFactory(unitOfWork);

            //ITableFilter<Device> filter = devicesTableFilterFactory.CreateTableFilter();


            //var filterPairs = new Dictionary<string, string>() {
            //    {"Processor","PROCESOR-NAME" },
            //    {"OperatingSystemId","5" }
            //};
            //var results = await filter.Filter(filterPairs);
            try
            {
                var totalDevices = await unitOfWork.DeviceRepository.GetDevicesListWithUsersAndOperatingSystemAsync();

                return deviceMapper.MapDeviceEntityToDeviceDTO(totalDevices);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
