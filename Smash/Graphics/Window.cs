using System.Numerics;
using SDL3;

namespace Smash.Graphics;

public class Window
{
    public nint Handle;

    public float Width;
    public float Height;
    public Vector2 Bounds
    {
        get { return new Vector2(Width, Height); }
        set { Width = value.X; Height = value.Y; }
    }

    public Window(nint windowHandle)
    {
        Handle = windowHandle;
    }

    public void Destroy()
    {
        SDL.DestroyWindow(Handle);
    }

    public void OnWindowResized(object? e, WindowResizeEventArgs eventArgs)
    {
        if (eventArgs.WindowId == SDL.GetWindowID(Handle))
        {
            SDL.GetWindowSize(Handle, out int _width, out int _height);
            Width = _width;
            Height = _height;
        }
    }
}
