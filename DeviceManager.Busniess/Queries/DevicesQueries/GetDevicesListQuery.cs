using DeviceManager.Busniess.Dtos;
using MediatR;
using System.Collections.Generic;

namespace DeviceManager.Busniess.Queries.DevicesQueries
{
    public record GetDevicesListQuery:IRequest<IEnumerable<DeviceDTO>>;
}
