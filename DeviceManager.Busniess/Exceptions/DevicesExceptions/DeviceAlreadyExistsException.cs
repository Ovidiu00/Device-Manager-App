using DeviceManager.Busniess.Exceptions.BaseException;

namespace DeviceManager.Busniess.Exceptions.DevicesExceptions
{
    public class DeviceAlreadyExistsException : AplicationBaseException
    {
        private const string DefaultMessage = "Device already exists!";
        public DeviceAlreadyExistsException() : base(DefaultMessage)
        {

        }
        public DeviceAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
