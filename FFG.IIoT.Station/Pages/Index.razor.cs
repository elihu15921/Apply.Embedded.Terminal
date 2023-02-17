namespace IIoT.Station.Pages;
public partial class Index
{
    async Task DisplayGreetingAlert()
    {
        var authState = await AuthenticationState;
        var message = $"Hello {authState.User.Identity?.Name}";
        await js.InvokeVoidAsync("alert", message);
    }
    [CascadingParameter] Task<AuthenticationState> AuthenticationState { get; set; }
}