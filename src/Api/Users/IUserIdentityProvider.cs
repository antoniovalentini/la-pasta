namespace LaPasta.Apis.Users;

public interface IUserIdentityProvider
{
    Task<string> GetCurrentUserIdAsync();
}

public class TestUserIdentityProvider : IUserIdentityProvider
{
    private const string UserId = "123";

    public async Task<string> GetCurrentUserIdAsync()
    {
        return await Task.FromResult(UserId);
    }
}
