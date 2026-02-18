using System.Numerics;
using SDL3;

namespace Smash.Input;

public class InputHandler
{
    public static float MouseX { get; private set; }
    public static float MouseY { get; private set; }
    public static Vector2 MousePosition => new Vector2(MouseX, MouseY);

    public static float ScrollWheelDelta { get; private set; }

    private static HashSet<SDL.Keycode> _downKeys = new();
    private static HashSet<SDL.Keycode> _pressedKeys = new();

    private static bool _leftMouseDown;
    private static bool _rightMouseDown;
    private static bool _leftMousePressed;
    private static bool _rightMousePressed;

    public void Event(SDL.Event e)
    {
        if (e.Type == (uint)SDL.EventType.KeyDown && !e.Key.Repeat)
        {
            _downKeys.Add(e.Key.Key);
            _pressedKeys.Add(e.Key.Key);
        }
        else if (e.Type == (uint)SDL.EventType.KeyUp)
        {
            _downKeys.Remove(e.Key.Key);
        }

        if (e.Type == (uint)SDL.EventType.MouseButtonDown)
        {
            if (e.Button.Button == SDL.ButtonLeft)
            {
                _leftMousePressed = !_leftMouseDown;
                _leftMouseDown = true;
            }

            if (e.Button.Button == SDL.ButtonRight)
            {
                _rightMousePressed = !_rightMouseDown;
                _rightMouseDown = true;
            }
        }

        if (e.Type == (uint)SDL.EventType.MouseButtonUp)
        {
            if (e.Button.Button == SDL.ButtonLeft)
            {
                _leftMouseDown = false;
            }

            if (e.Button.Button == SDL.ButtonRight)
            {
                _rightMouseDown = false;
            }
        }

        if (e.Type == (uint)SDL.EventType.MouseMotion)
        {
            SDL.GetMouseState(out float x, out float y);
            MouseX = x;
            MouseY = y;
        }
        
        if (e.Type == (uint)SDL.EventType.MouseWheel)
        {
            ScrollWheelDelta = e.Wheel.Y;
        }
    }

    public void Update()
    {
        _pressedKeys.Clear();
        _leftMousePressed = false;
        _rightMousePressed = false;
        ScrollWheelDelta = 0;
    }

    public static bool IsKeyDown(SDL.Keycode key) => _downKeys.Contains(key);
    public static bool IsKeyPressed(SDL.Keycode key) => _pressedKeys.Contains(key);

    public static bool IsLeftMouseDown() => _leftMouseDown;
    public static bool IsRightMouseDown() => _rightMouseDown;
    public static bool IsLeftMousePressed() => _leftMousePressed;
    public static bool IsRightMousePressed() => _rightMousePressed;
}