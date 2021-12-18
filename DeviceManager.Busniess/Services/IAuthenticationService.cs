namespace DeviceManager.Busniess.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(string email, string password);
    }
}
