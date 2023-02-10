namespace IIoT.Station.Pages;
public partial class Login
{
    async Task Authenticate()
    {
        var userAccount = userAccountService.GetByUserName(model.UserName);
        if (userAccount is null || userAccount.Password != model.Password)
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
    class Model
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    Model model = new Model();
}