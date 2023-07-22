using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace BTitles;

/**
 * Base class for titles
 */
public abstract class BiomeTitle : UIElement
{
    public virtual string Text { get; set; }
    public virtual DynamicSpriteFont Font { get; set; }
    public virtual float FontScale { get; set; }
    public virtual float Scale { get; set; }
    public virtual Color TextColor { get; set; }
    public virtual Color? TextStrokeColor { get; set; }
    public virtual Texture2D Icon { get; set; }
    public virtual Vector2 ContentOffset { get; set; }
    public virtual float Opacity { get; set; }
    
    public virtual Action<BiomeTitle> CustomReset { get; set; }

    public virtual void SetBackgroundEnabled(bool enabled)
    {
        
    }
    
    public virtual void SetWidthHeightAuto()
    {
        
    }
    
    public virtual void Reset()
    {
        Text = "";
        Font = FontAssets.MouseText.Value;
        FontScale = 1;
        TextColor = Color.White;
        TextStrokeColor = null;
        Icon = null;
        ContentOffset = Vector2.Zero;
        Opacity = 1;

        Left.Set(0, 0);
        Top.Set(0, 0);
        Width.Set(0, 0);
        Height.Set(0, 0);
        MinWidth.Set(0, 0);
        MinHeight.Set(0, 0);
        MaxWidth.Set(100000, 0);
        MaxHeight.Set(100000, 0);
        MarginLeft = 0;
        MarginRight = 0;
        MarginTop = 0;
        MarginBottom = 0;

        CustomReset?.Invoke(this);

        Recalculate();
    }
}