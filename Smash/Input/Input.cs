using System.Numerics;
using System.Runtime.InteropServices;
using SDL3;

namespace Smash.Input;

public static class InputHandler
{
    public static float MouseX { get; private set; }
    public static float MouseY { get; private set; }
    public static Vector2 MousePosition => new Vector2(MouseX, MouseY);

    public static float ScrollWheelDelta { get; private set; }

    /// <summary>
    /// The characters the user has entered this frame. Start polling for text input by calling InputHandler.StartPollingTextInput()
    /// </summary>
    public static string? TextInput { get; private set; }

    private static HashSet<SDL.Keycode> _previousDownKeys = new();
    private static HashSet<SDL.Keycode> _previousPressedKeys = new();

    private static HashSet<SDL.Keycode> _downKeys = new();
    private static HashSet<SDL.Keycode> _pressedKeys = new();

    private static bool _leftMouseDown;
    private static bool _rightMouseDown;
    private static bool _leftMousePressed;
    private static bool _rightMousePressed;

    private static bool _listenForTextInput = false;

    public static void Event(SDL.Event e)
    {
        _previousDownKeys = _downKeys;
        _previousPressedKeys = _pressedKeys;

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

        if (_listenForTextInput)
        {
            if (e.Type == (uint)SDL.EventType.TextInput)
            {
                TextInput = Marshal.PtrToStringUTF8(e.Text.Text);
            }
        }
    }

    public static void Update()
    {
        _pressedKeys.Clear();
        _leftMousePressed = false;
        _rightMousePressed = false;
        TextInput = null;
        ScrollWheelDelta = 0;
    }

    public static void StartPollingTextInput()
    {
        if (!SmashEngine._pollingTextInput)
        {
            SmashEngine.StartPollingTextInput();
        }

        _listenForTextInput = true;
    }

    public static bool IsKeyDown(SDL.Keycode key) => _downKeys.Contains(key);
    public static bool IsKeyPressed(SDL.Keycode key) => _pressedKeys.Contains(key);

    public static bool WasKeyDown(SDL.Keycode key) => _previousDownKeys.Contains(key);
    public static bool WasKeyPressed(SDL.Keycode key) => _previousPressedKeys.Contains(key);

    public static bool IsLeftMouseDown() => _leftMouseDown;
    public static bool IsRightMouseDown() => _rightMouseDown;
    public static bool IsLeftMousePressed() => _leftMousePressed;
    public static bool IsRightMousePressed() => _rightMousePressed;
}