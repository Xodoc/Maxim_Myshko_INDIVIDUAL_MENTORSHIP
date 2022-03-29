namespace AuthenticationServer.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(string email, string password);
    }
}
