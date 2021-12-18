using DeviceManager.Busniess.Dtos;
using MediatR;
using System.Collections.Generic;

namespace DeviceManager.Busniess.Queries
{
    public record GetAvailableOperatingSystemsQuery:IRequest<IEnumerable<OperatingSystemDTO>>;
}
