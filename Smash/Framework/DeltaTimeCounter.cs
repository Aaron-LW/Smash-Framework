using SDL3;

namespace Smash;

public sealed class DeltaTimeCounter
{
    private ulong _lastCounter;
    private readonly ulong _frequency;

    /// <summary>
    /// DeltaTime in Seconds
    /// </summary>
    public double Seconds { get; private set; }

    /// <summary>
    /// DeltaTime in Milliseconds
    /// </summary>
    public double Milliseconds => Seconds * 1000.0;

    private double _maxDeltaSeconds { get; set; } = 0.05;

    public DeltaTimeCounter()
    {
        _frequency = SDL.GetPerformanceFrequency();
        if (_frequency == 0)
            throw new InvalidOperationException("SDL performance frequency is zero.");

        _lastCounter = SDL.GetPerformanceCounter();
        Seconds = 0.0;
    }

    public void Update()
    {
        ulong current = SDL.GetPerformanceCounter();
        ulong delta = current - _lastCounter;

        Seconds = delta / (double)_frequency;

        if (Seconds > _maxDeltaSeconds)
            Seconds = _maxDeltaSeconds;

        _lastCounter = current;
    }
}
