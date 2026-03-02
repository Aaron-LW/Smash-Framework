using SDL3;

public static class EventHandler
{
    public static bool PollEvent(out Event e)
    {
        e = null!;

        if (SDL.PollEvent(out SDL.Event sdle))
        {
            e = new Event(sdle);
            return true;
        }
        else return false;
    }
}