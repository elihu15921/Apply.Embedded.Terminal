namespace IIoT.Station.Shared;
public partial class MainLayout
{
    void ToggleDrawer() => Open = !Open;
    bool Open { get; set; } = false;
    bool PreserveOpenState { get; set; } = false;
    Breakpoint Breakpoint { get; set; } = Breakpoint.Lg;
    DrawerClipMode ClipMode { get; set; } = DrawerClipMode.Always;
}