namespace GameAuditor.Services.UserService
{
    public interface IUserService
    {
        string GetMyName();
        string GetMyId();

        string GetCookiesRefreshToken();
    }
}
