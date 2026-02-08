using SDL3;
using Smash.Graphics;

namespace Smash;

public abstract class Application
{
    private List<nint> _windowHandles = [];

    public abstract void Start();
    public abstract void Update(double deltaTime);
    public abstract void Render();
    public abstract void End();

    protected void CreateWindowAndRenderer(string title, int width, int height, out Window window, out Renderer renderer)
    {
        if (!SDL.CreateWindowAndRenderer(title, width, height, 0, out nint windowHandle, out nint rendererHandle))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            throw new Exception($"SDL ERROR: Error creating window and rendering: {SDL.GetError}");
        }

        SDL.SetRenderVSync(rendererHandle, 1);

        window = new Window(windowHandle);
        renderer = new Renderer(rendererHandle);
        _windowHandles.Add(windowHandle);
    }

    protected void TTFInit()
    {
        if (!TTF.Init())
        {
            throw new Exception($"TTF was not able to init: {SDL.GetError()}");
        }
    }

    protected void TTFDestroy()
    {
        TTF.Quit();
    }
}    