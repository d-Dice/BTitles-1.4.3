using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BTitles;

/**
 * Information required to display title
 */
public class BiomeEntry
{
    // Biome display title
    public string Title = "";

    public Color TitleColor = Color.White;
    
    public Color StrokeColor = Color.Black;
    
    // UI element to be rendered behind the title, null for default
    public BiomeTitle TitleWidget = null;

    // Text to be displayed under the title, can be any but intended to be a mod name
    public string SubTitle = "";

    // UI element to be rendered behind subtitle, null for default
    public BiomeTitle SubTitleWidget = null;

    // Biome icon to be rendered near title, null for none
    public Texture2D Icon = null;
}