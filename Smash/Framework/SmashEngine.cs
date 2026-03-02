using SDL3;
using Smash.Graphics;
using Smash.Input;

namespace Smash;

public static class SmashEngine
{
    public static double DeltaTime => _deltaTimeCounter.Seconds;

    internal static DeltaTimeCounter _deltaTimeCounter = new();
    internal static List<Window> _windows = new();

    public static void Init()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            throw new Exception($"SDL couldn't initialize: {SDL.GetError()}");
        }

        if (!TTF.Init())
        {
            throw new Exception($"TTF couldn't initialize: {SDL.GetError()}");
        }
    }

    public static void Update(bool claimEvents = true)
    {
        _deltaTimeCounter.Update();
        InputHandler.Update();

        if (claimEvents)
        {
            while (SDL.PollEvent(out SDL.Event e))
            {
                ProcessEvent(e);
            }
        }
    }

    public static void ProcessEvent(SDL.Event e)
    {
        if (e.Type == (uint)SDL.EventType.WindowResized)
        {
            foreach (Window window in _windows)
            {
                if (e.Window.WindowID == SDL.GetWindowID(window.Handle))
                {
                    SDL.GetWindowSize(window.Handle, out int _width, out int _height);
                    window.Width = _width;
                    window.Height = _height;
                }
            }
        }

        if (e.Type == (uint)SDL.EventType.Quit)
        {
            Application._applicationShouldClose = true;
        }

        InputHandler.Event(e);
    }

    public static void Stop()
    {
        SDL.Quit();
        TTF.Quit();
    }
}
