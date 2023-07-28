using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;

namespace BTitles;

public interface IBasicBiomeTitleBackground
{
    public float DesiredHeight { get; }
    public float LeftContentPadding { get; set; }
    public float RightContentPadding { get; set; }
    public float Opacity { get; set; }
    public float Scale { get; set; }
}

/**
 * Default implementation of BiomeTitle
 */
public class BasicBiomeTitle : BiomeTitle
{
    public override float Opacity
    {
        get => _opacity;
        set
        {
            _opacity = value;

            if (Background is IBasicBiomeTitleBackground panel)
            {
                panel.Opacity = value;
            }
        }
    }
    
    public override float Scale
    {
        get => _scale;
        set
        {
            _scale = value;

            if (Background is IBasicBiomeTitleBackground panel)
            {
                panel.Scale = value;
            }
        }
    }
    
    public float IconTextSpace { get; set; }

    public UIElement Background
    {
        get => _background;
        set
        {
            bool enabled = BackgroundEnabled;
            if (enabled)
            {
                SetBackgroundEnabled(false);
            }

            _background = value;

            if (enabled)
            {
                SetBackgroundEnabled(true);
            }
        }
    }

    public bool BackgroundEnabled => Background != null && Background.Parent == this;

    protected Vector2 CachedIconSize;
    protected Vector2 CachedTextSize;
    protected bool CachedHasText;
    protected Vector2 CachedContentSize;
    
    private float _opacity = 1;
    private float _scale = 1;
    private UIElement _background = null;

    public override void SetBackgroundEnabled(bool enabled)
    {
        if (Background == null) return;

        if (enabled)
        {
            if (Background.Parent == this) return;
            Append(Background);
        }
        else
        {
            if (Background.Parent != this) return;
            RemoveChild(Background);
        }
    }

    public override void SetWidthHeightAuto()
    {
        RecalculateContent();
        
        Vector2 calculatedSize = CachedContentSize;
        
        if (BackgroundEnabled)
        {
            if (Background is IBasicBiomeTitleBackground basicBackground)
            {
                calculatedSize.X += (basicBackground.LeftContentPadding + basicBackground.RightContentPadding) * basicBackground.Scale;
                calculatedSize.Y = Math.Max(calculatedSize.Y, basicBackground.DesiredHeight);
            }
        }
        
        Width.Set(calculatedSize.X, 0);
        Height.Set(calculatedSize.Y, 0);
    }

    public override void Reset()
    {
        IconTextSpace = 10;
        
        base.Reset();
    }

    public void RecalculateIcon()
    {
        CachedIconSize = new Vector2(Icon?.Width ?? 0, Icon?.Height ?? 0) * Scale;
    }
    
    public void RecalculateText()
    {
        CachedHasText = !string.IsNullOrWhiteSpace(Text);
        CachedTextSize = (Font.MeasureString(Text) - new Vector2(0, 9)) * FontScale * Scale;
    }

    public void RecalculateContent()
    {
        RecalculateIcon();
        RecalculateText();
        
        CachedContentSize = new Vector2(CachedTextSize.X + CachedIconSize.X, Math.Max(CachedTextSize.Y, CachedIconSize.Y));
        if (CachedHasText && Icon != null) CachedContentSize.X += IconTextSpace * Scale;
    }

    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        base.DrawChildren(spriteBatch);
        
        PostDrawChildren(spriteBatch);
    }

    protected void PostDrawChildren(SpriteBatch spriteBatch)
    {
        var dimensions = GetDimensions();

        float contentX = dimensions.X + (dimensions.Width - CachedContentSize.X) / 2;

        if (Icon != null)
        {
            spriteBatch.Draw(Icon, new Vector2(contentX, dimensions.Y + (dimensions.Height - CachedIconSize.Y) / 2) + ContentOffset * Scale, new Rectangle(0, 0, Icon.Width, Icon.Height), Color.White * Opacity, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
            contentX += CachedIconSize.X + IconTextSpace * Scale;
        }

        if (CachedHasText)
        {
            Vector2 textPosition = new Vector2(contentX, dimensions.Y + (dimensions.Height - CachedTextSize.Y) / 2) + ContentOffset * Scale;
            
            if (TextStrokeColor.HasValue)
            {
                spriteBatch.DrawString(Font, Text, textPosition + new Vector2(+1, 0), TextStrokeColor.Value * Opacity, 0, Vector2.Zero, FontScale * Scale, SpriteEffects.None, 0);
                spriteBatch.DrawString(Font, Text, textPosition + new Vector2(-1, 0), TextStrokeColor.Value * Opacity, 0, Vector2.Zero, FontScale * Scale, SpriteEffects.None, 0);
                spriteBatch.DrawString(Font, Text, textPosition + new Vector2(0, +1), TextStrokeColor.Value * Opacity, 0, Vector2.Zero, FontScale * Scale, SpriteEffects.None, 0);
                spriteBatch.DrawString(Font, Text, textPosition + new Vector2(0, -1), TextStrokeColor.Value * Opacity, 0, Vector2.Zero, FontScale * Scale, SpriteEffects.None, 0);
            }
            
            spriteBatch.DrawString(Font, Text, textPosition, TextColor * Opacity, 0, Vector2.Zero, FontScale * Scale, SpriteEffects.None, 0);
        }
    }
}