using SDL3;
using Smash.Graphics;

namespace Smash;

public abstract class Application
{
    public virtual void Start() {}
    public abstract void Update(double deltaTime);
    public abstract void Render();
    public virtual void End() { SDL.Quit(); }

    protected void CreateWindowAndRenderer(string title, int width, int height, out Window window, out Renderer renderer)
    {
        if (!SDL.CreateWindowAndRenderer(title, width, height, 0, out nint windowHandle, out nint rendererHandle))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            throw new Exception($"SDL ERROR: Error creating window and rendering: {SDL.GetError}");
        }

        window = new Window(windowHandle);
        renderer = new Renderer(rendererHandle);
    }

    /// <summary>
    /// Loads a Font relative to the root directory of the project
    /// </summary>
    /// <param name="relativePath">The relative path from the root of the project</param>
    /// <returns></returns>
    protected Font LoadFont(string relativePath, float pointSize)
    {
        string fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

        if (!File.Exists(fontPath))
        {
            throw new FileNotFoundException("Couldn't find Font at " + fontPath);
        }

        nint fontHandle = TTF.OpenFont(fontPath, pointSize);
        return new Font(fontHandle, pointSize);
    }
}    