using System.Drawing;
using Smash.Graphics;

namespace CreateWindow;

internal static class Program
{
    private static void Main(string[] args)
    {
        Window window = Window.CreateWindow("Create window example :D", 800, 600);
        Renderer renderer = Renderer.CreateRenderer(window);

        bool running = true;
        while (running)
        {
            while (EventHandler.PollEvent(out Event e))
            {
                if (e.EventType == EventType.Quit)
                {
                    running = false;
                }
            }

            renderer.Clear(Color.CornflowerBlue);
            renderer.RenderPresent();
        }
    }
}