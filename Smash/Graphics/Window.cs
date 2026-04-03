using System.Numerics;
using SDL3;

namespace Smash.Graphics;

public class Window : IDisposable
{
    /// <summary>
    /// The handle of this instance of the Window class
    /// </summary>
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

    public void SetFullscreen(bool fullscreen)
    {
        SDL.SetWindowFullscreen(Handle, fullscreen);
    }

    public void SetWindowAlwaysOnTop(bool alwaysOnTop)
    {
        SDL.SetWindowAlwaysOnTop(Handle, alwaysOnTop);
    }

    public void SetWindowTitle(string title)
    {
        SDL.SetWindowTitle(Handle, title);
    }

    public void SetWindowMinimumSize(int minimumWidht, int minimumHeight)
    {
        SDL.SetWindowMinimumSize(Handle, minimumWidht, minimumHeight);
    }

    public void SetWindowMaximumSize(int maximumWidth, int maximumHeight)
    {
        SDL.SetWindowMaximumSize(Handle, maximumWidth, maximumHeight);
    }

    public void SetWindowResizable(bool resizable)
    {
        SDL.SetWindowResizable(Handle, resizable);
    }

    public void Dispose()
    {
        SDL.DestroyWindow(Handle);
    }
}