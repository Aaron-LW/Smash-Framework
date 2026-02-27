using Smash.Graphics;
using SDL3;

namespace Smash.Graphics;

public static class AssetManager
{
    public static Dictionary<string, Texture2D> LoadedTextures { get; private set; } = new();

    private static string _rootDirectoryPath = "";

    /// <summary>
    /// Sets the relative path from which all assets will be loaded
    /// </summary>
    /// <param name="directoryPath"></param>
    public static void SetAssetRootDirectory(string directoryPath)
    {
        _rootDirectoryPath = directoryPath;
    }

    /// <summary>
    /// Loads a Texture2D from an image relative to the root directory path
    /// The loaded Texture2D will be stored into the LoadedTextures dictionary with the key being the files name without the extension
    /// </summary>
    /// <returns>The loaded Texture2D</returns>
    public static Texture2D LoadTexture(string relativePath, Renderer renderer)
    {
        string fullPath = Path.Combine(_rootDirectoryPath, relativePath);
        string fileName = Path.GetFileNameWithoutExtension(fullPath);

        nint textureHandle = Image.LoadTexture(renderer.Handle, fullPath);
        Texture2D texture = new Texture2D(textureHandle, fileName);

        LoadedTextures.Add(fileName, texture);
        return texture;
    }

    public static void DestroyAllTextures()
    {
        foreach (Texture2D texture in LoadedTextures.Values)
        {
            SDL.DestroyTexture(texture.Handle);
        }
    }
}