using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

public class CustomUIImage : UIElement
{
    private Texture2D _texture;
    public float Opacity { get; set; }

    public CustomUIImage(Texture2D texture)
    {
        _texture = texture;
        Opacity = 1f;
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, GetDimensions().Position(), Color.White * Opacity);
    }
}