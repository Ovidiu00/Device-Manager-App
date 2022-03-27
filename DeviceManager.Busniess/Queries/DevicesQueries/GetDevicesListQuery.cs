using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Query_Filter_Model.Custom;
using MediatR;
using System.Collections.Generic;

namespace DeviceManager.Busniess.Queries.DevicesQueries
{
    public record GetDevicesListQuery(DevicesFilterModel query) :IRequest<IEnumerable<DeviceDTO>>;
}
