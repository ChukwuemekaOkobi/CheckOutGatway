namespace Services.Implementation
{
    public interface IAuthentication
    {
        bool Authenticate(string apiKey);
    }
}