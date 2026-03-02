using System.Drawing;
using SDL3;
using Smash.Graphics;

internal static class Program
{
    private static void Main(string[] args)
    {
        Window window = Window.CreateWindow("Create window example :D", 800, 600);
        Renderer renderer = Renderer.CreateRenderer(window);

        bool running = true;
        while (running)
        {
            while (SDL.PollEvent(out SDL.Event e))
            {
                if (e.Type == (uint)SDL.EventType.Quit)
                {
                    running = false;
                }
            }

            renderer.Clear(Color.CornflowerBlue);
            renderer.RenderPresent();
        }
    }
}