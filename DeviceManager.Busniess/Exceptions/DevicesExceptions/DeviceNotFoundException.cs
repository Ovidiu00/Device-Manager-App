
using DeviceManager.Busniess.Exceptions.BaseException;

namespace DeviceManager.Busniess.Exceptions.DevicesExceptions
{
    public class DeviceNotFoundException : AplicationBaseException
    {
        private const string DefaultMessage = "Cant find given device!";
        public DeviceNotFoundException() : base(DefaultMessage)
        {

        }
        public DeviceNotFoundException(string message) : base(message)
        {

        }
    }
}
