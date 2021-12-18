using AutoMapper;
using DeviceManager.Busniess.Dtos;
using DeviceManager.Busniess.Queries;
using DeviceManager.DataAcess.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.Handler
{
    public class GetAvailableOperatingSystemsHandler : IRequestHandler<GetAvailableOperatingSystemsQuery, IEnumerable<OperatingSystemDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAvailableOperatingSystemsHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<OperatingSystemDTO>> Handle(GetAvailableOperatingSystemsQuery request, CancellationToken cancellationToken)
        {
            var avaialbleOperatingSystems = await unitOfWork.OperatingSystemsRepository.GetAll();

            return mapper.Map<IEnumerable<OperatingSystemDTO>>(avaialbleOperatingSystems);        
        }
    }
}
