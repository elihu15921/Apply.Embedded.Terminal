namespace IIoT.Station.Pages;
public partial class Index
{
    async Task DisplayGreetingAlert()
    {
        var authState = await authenticationState;
        var message = $"Hello {authState.User.Identity.Name}";
        await js.InvokeVoidAsync("alert", message);
    }
    [CascadingParameter] Task<AuthenticationState> authenticationState { get; set; }
}