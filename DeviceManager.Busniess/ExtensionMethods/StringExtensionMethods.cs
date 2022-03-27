using DeviceManager.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManager.Busniess.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static DeviceType ConvertToDeviceType(this string str)
        {
            switch (str)
            {
                case "0":return DeviceType.PHONE;
                case "1": return DeviceType.TABLET;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
