using Smash.Graphics;
using SDL3;

namespace Smash.Graphics;

public static class AssetManager
{
    private static Dictionary<string, Texture2D> _loadedTextures = new();

    private static string _rootDirectoryPath = "";
    private static BlendMode _defaultBlendMode = BlendMode.Blend;
    private static ScaleMode _defaultScaleMode = ScaleMode.Linear;

    public static Texture2D? TryGet(string textureName)
    {
        _loadedTextures.TryGetValue(textureName, out Texture2D? texture);
        return texture;
    }

    public static Texture2D Get(string textureName)
    {
        return _loadedTextures[textureName];
    }

    /// <summary>
    /// Sets the relative path from which all assets will be loaded
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void SetAssetRootDirectory(string directoryPath)
    {
        _rootDirectoryPath = directoryPath;
    }

    /// <summary>
    /// Sets the default blend mode that should be applied to every loaded Texture2D after this function has been called
    /// </summary>
    public static void SetDefaultBlendMode(BlendMode blendMode)
    {
        _defaultBlendMode = blendMode;
    }

    /// <summary>
    /// Sets the default scale mode that should be applied to every loaded Texture2D after this function has been called
    /// </summary>
    public static void SetDefaultScaleMode(ScaleMode scaleMode)
    {
        _defaultScaleMode = scaleMode;
    }

    /// <summary>
    /// Loads a Texture2D from an image relative to the root directory path
    /// The loaded Texture2D will be stored into the _loadedTextures dictionary with the key being the files name without the extension
    /// </summary>
    /// <returns>The loaded Texture2D</returns>
    public static Texture2D LoadTexture(string relativePath, Renderer renderer)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, relativePath);
        string fileName = Path.GetFileNameWithoutExtension(fullPath);

        nint textureHandle = Image.LoadTexture(renderer.Handle, fullPath);
        Texture2D texture = new Texture2D(textureHandle, fileName);

        SDL.SetTextureBlendMode(texture.Handle, (SDL.BlendMode)_defaultBlendMode);
        SDL.SetTextureScaleMode(texture.Handle, (SDL.ScaleMode)_defaultScaleMode);

        _loadedTextures.Add(fileName, texture);
        return texture;
    }

    public static void AddTextureRegion(string name, TextureRegion textureRegion)
    {
        _loadedTextures.TryGetValue(textureRegion.BaseTextureName, out Texture2D? baseTexture);
        if (baseTexture == null) throw new Exception($"""Texture with name "{name}" cannot be found""");

        _loadedTextures.Add(name, new Texture2D(baseTexture.Handle, name, new Rectangle(textureRegion.X, textureRegion.Y, textureRegion.Width, textureRegion.Height)));
    }

    public static void DestroyAllTextures()
    {
        foreach (Texture2D texture in _loadedTextures.Values)
        {
            SDL.DestroyTexture(texture.Handle);
        }
    }
}