using System.Numerics;
using SDL3;

namespace Smash.Graphics;

public class Window
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

    /// <summary>
    /// Destroys this window
    /// </summary>
    public void Destroy()
    {
        SDL.DestroyWindow(Handle);
    }
}