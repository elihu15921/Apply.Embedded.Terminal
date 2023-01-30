namespace IIoT.Station.Pages;

[IgnoreAntiforgeryToken, ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class Error : PageModel
{
    readonly ILogger<Error> _logger;
    public Error(ILogger<Error> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public string? RequestId { get; set; }
}