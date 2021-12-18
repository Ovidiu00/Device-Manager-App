using AutoMapper;
using DeviceManager.API.ViewModels;
using DeviceManager.Busniess.Dtos;
using System;
using System.Collections.Generic;

namespace DeviceManager.API.AutoMapperApi
{
    public static class AutoMapperExtensions
    {
        static private AutoMapperApiProfile myProfile = new AutoMapperApiProfile();
        static private MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        static private Mapper mapper = new Mapper(configuration);

        public static DeviceVM MapDeviceDTO_To_DeviceVM(this IMapper mapper, DeviceDTO source)
        {
            try
            {
                var deviceViewModel = mapper.Map<DeviceVM>(source);

                deviceViewModel.OperatingSystem = source.OperatingSystem?.Name ?? "NO OS";
                deviceViewModel.OS_Version = source.OperatingSystem?.Version ?? 0;
                deviceViewModel.UsersName = source.User?.UserName ?? "NO USER";
                deviceViewModel.UserId = source.User?.Id ?? "";

                return deviceViewModel;
            }
            catch (Exception)
            {
                throw;
            }
            



        }
    }
}
