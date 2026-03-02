using SDL3;

public class Event
{
    public readonly EventType EventType;

    internal Event(SDL.Event e)
    {
        EventType = (EventType)e.Type;
    }
}