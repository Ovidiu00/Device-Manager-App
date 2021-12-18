using AutoMapper;
using DeviceManager.API.ViewModels;
using DeviceManager.Busniess.Dtos;

namespace DeviceManager.API
{
    public class AutoMapperApiProfile : Profile
    {
        public AutoMapperApiProfile()
        {
            CreateMap<DeviceDTO, DeviceVM>();
            CreateMap<AddDeviceVM, AddDeviceDTO>();
            CreateMap<EditDeviceVM, EditDeviceDTO>();
        }
    }
}
