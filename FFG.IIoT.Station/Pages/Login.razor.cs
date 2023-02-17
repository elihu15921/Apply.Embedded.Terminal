namespace IIoT.Station.Pages;
public partial class Login
{
    async Task Authenticate()
    {
        var userAccount = userAccountService.GetByUserName(Model?.UserName);
        if (userAccount is null || userAccount.Password != Model?.Password)
        {
            await js.InvokeVoidAsync("alert", "Invalid User Name or Password");
            return;
        }
        var customAuthStateProvider = (CustomStateProvider)authStateProvider;
        await customAuthStateProvider.UpdateAuthenticationState(new UserSession
        {
            UserName = userAccount.UserName,
            Role = userAccount.Role
        });
        navManager.NavigateTo("/", forceLoad: true);
    }
    class Request
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
    Request Model { get; set; } = new();
}