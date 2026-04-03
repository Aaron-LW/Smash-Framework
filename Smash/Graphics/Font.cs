using System.Numerics;
using SDL3;
using Smash;

public class Font : IDisposable
{
    public nint Handle { get; }

    public float PointSize { get; }

    internal Dictionary<string, nint> _alreadyCreatedTexts = new();

    public Font(nint fontHandle, float pointSize)
    {
        Handle = fontHandle;
    }

    public Vector2 MeasureString(string text)
    {
        TTF.GetTextSize(GetOrCreateText(text), out int widht, out int height);
        return new Vector2(widht, height);
    }

    internal nint GetOrCreateText(string text)
    {
        if (_alreadyCreatedTexts.TryGetValue(text, out nint textObject))
        {
            return textObject;
        }

        nint textObjectHandle = TTF.CreateText(SmashEngine._fontEngine, Handle, text, (nuint)text.Length);
        _alreadyCreatedTexts[text] = textObjectHandle;

        return textObjectHandle;
    }

    public void Dispose()
    {
        foreach (nint alreadyCreatedText in _alreadyCreatedTexts.Values)
        {
            TTF.DestroyText(alreadyCreatedText);
        }

        TTF.CloseFont(Handle);
        _alreadyCreatedTexts.Clear();
    }
}