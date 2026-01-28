namespace Smash;

public class WindowResizeEventArgs : EventArgs
{
    public uint WindowId;

    public WindowResizeEventArgs(uint windowId)
    {
        WindowId = windowId;
    }
}