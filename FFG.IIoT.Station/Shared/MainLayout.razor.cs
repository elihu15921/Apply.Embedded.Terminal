namespace IIoT.Station.Shared;
public partial class MainLayout
{
    bool Open = false;
    readonly bool PreserveOpenState = false;
    readonly Breakpoint Breakpoint = Breakpoint.Lg;
    readonly DrawerClipMode ClipMode = DrawerClipMode.Always;
    void ToggleDrawer()
    {
        Open = !Open;
    }
}