using SDL3;

internal static class FontEngine
{
    public static nint Handle;

    private static Dictionary<string, nint> _texts = new Dictionary<string, nint>();
    private static List<nint> _fontHandles = new();

    internal static nint GetTextHandleFromText(string text)
    {
        if (_texts.TryGetValue(text, out nint textHandle))
        {
            return textHandle;
        }

        nint textHandle2 = TTF.CreateText(Handle, _fontHandles[0], text, 0);
        return textHandle2;
    }

    
}