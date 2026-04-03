using SDL3;
using Smash;

public class Font
{
    public nint Handle { get; }
    
    public float PointSize { get; }

    internal Dictionary<string, nint> _alreadyCreatedTexts = new();

    public Font(nint fontHandle, float pointSize)
    {
        Handle = fontHandle;
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
}