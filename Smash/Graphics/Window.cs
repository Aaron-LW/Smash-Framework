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

    /// <summary>
    /// Event that should be called when the window resizes
    /// </summary>
    /// <param name="e">The object</param>
    /// <param name="eventArgs">The WindowResizeEventArgs containing the new width and height</param>
    public void OnWindowResized(object? e, WindowResizeEventArgs eventArgs)
    {
        if (eventArgs.WindowId == SDL.GetWindowID(Handle))
        {
            SDL.GetWindowSize(Handle, out int _width, out int _height);
            Width = _width;
            Height = _height;
        }
    }

    public static Window CreateWindow(string title, int width, int height)
    {
        nint windowHandle = SDL.CreateWindow(title, width, height, SDL.WindowFlags.Vulkan);
        return new Window(windowHandle);
    }
}
