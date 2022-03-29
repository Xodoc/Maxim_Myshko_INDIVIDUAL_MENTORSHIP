namespace AuthenticationServer.Interfaces
{
    public interface IAuthorizationService
    {
        Task<string> Authenticate(string email, string password);
    }
}
