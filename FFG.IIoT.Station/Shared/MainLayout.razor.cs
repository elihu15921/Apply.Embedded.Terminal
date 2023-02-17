namespace IIoT.Station.Shared;
public partial class MainLayout
{
    void ToggleDrawer() => Open = !Open;
    bool Open { get; set; } = default;
    bool PreserveOpenState { get; set; } = default;
    Breakpoint Breakpoint { get; set; } = Breakpoint.Lg;
    DrawerClipMode ClipMode { get; set; } = DrawerClipMode.Always;
}