using AutoMapper;
using DeviceManager.Busniess.Dtos;
using DeviceManager.DataAcess.EF.Entities;

namespace DeviceManager.Busniess.Mapper
{
    public class AutoMapperContractProfile : Profile
    {
        public AutoMapperContractProfile()
        {

            CreateMap<Device, DeviceDTO>();
              

            CreateMap<AddDeviceDTO, Device>()
                .ForMember(x => x.OperatingSystem, opt => opt.Ignore());

            CreateMap<EditDeviceDTO, Device>()
                .ForMember(x => x.OperatingSystem, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<OperatingSystem, OperatingSystemDTO>();

        }
    }
}
