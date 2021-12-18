using System;

namespace DeviceManager.Busniess.Exceptions.BaseException
{
    public class AplicationBaseException : Exception
    {
        private const string DefaultMessage = "Server ERROR";
        public AplicationBaseException() : base(DefaultMessage)
        {

        }
        public AplicationBaseException(string message) : base(message)
        {

        }
    }
}
