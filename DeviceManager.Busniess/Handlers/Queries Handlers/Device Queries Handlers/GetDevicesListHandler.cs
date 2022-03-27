using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Queries.DevicesQueries;
using DeviceManager.Busniess.Services.Mapping.DeviceMapper;
using DeviceManager.DataAcess.Filters.Factories;
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
        private readonly IDeviceTableFilterFactory deviceTableFilterFactory;

        public GetDevicesListHandler(IUnitOfWork unitOfWork, IDeviceMapper deviceMapper, IDeviceTableFilterFactory deviceTableFilterFactory)
        {
            this.unitOfWork = unitOfWork;
            this.deviceMapper = deviceMapper;
            this.deviceTableFilterFactory = deviceTableFilterFactory;
        }
        public async Task<IEnumerable<DeviceDTO>> Handle(GetDevicesListQuery request, CancellationToken cancellationToken)
        {
            var page = request.query.Page;
            var itemsPerPage = request.query.ItemsPerPage;
            var selector = request.query.Selector;
            var value = request.query.Value;

            var devicesTableFilter = deviceTableFilterFactory.CreateTableFilter(selector);
            var filterExpression = devicesTableFilter.Filter(value);
          
            var devices = await unitOfWork.DeviceRepository.GetDevicesPaginated(page,itemsPerPage, filterExpression);

            return deviceMapper.MapDeviceEntityToDeviceDTO(devices);
        }
    }
}
