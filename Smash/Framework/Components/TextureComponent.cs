using Smash.Graphics;

namespace Smash.EntityComponentSystem;

public class TextureComponent : Component
{
    public Texture2D Texture;
    private string? TextureName;

    public TextureComponent(string textureName)
    {
        TextureName = textureName;
        Texture = TextureLoader.Get(textureName);
    }

    public TextureComponent(Texture2D texture)
    {
        Texture = texture;
    }

    public override Component Clone()
    {
        return new TextureComponent(Texture);
    }
}