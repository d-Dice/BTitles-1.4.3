using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace BTitles;

/**
 * Panel that has 3 segments
 */
public class SegmentedHorizontalPanel : UIElement, IBasicBiomeTitleBackground
{
    public enum GapRenderMethodType
    {
        /**
         * Part of texture between segments will be stretched to fill the space
         */
        Stretch,
        
        /**
         * Part of texture will be clipped, starting from cap segment, but will be stretched if necessary
         */
        FillFromCaps,
        
        /**
         * Part of texture will be clipped, starting from middle segment, but will be stretched if necessary
         */
        FillFromCenter
    }

    /**
     * Texture to be used
     */
    public Texture2D Texture;
    
    /**
     * Size of safe zone for left segment, relative to texture size (from 0 to 1)
     */
    public float LeftSegmentSize;
    
    /**
     * Size of safe zone for right segment, relative to texture size (from 0 to 1)
     */
    public float RightSegmentSize;
    
    /**
     * Size of safe zone for middle segment, relative to texture size (from 0 to 1)
     */
    public float MiddleSegmentSize;
    
    /**
     * Method that to be used to fill gaps between segments
     */
    public GapRenderMethodType GapRenderMethod;

    public float DesiredHeight => (Texture?.Height ?? 0) * Scale;
    public float LeftContentPadding { get; set; } = 0;
    public float RightContentPadding { get; set; } = 0;
    public float Opacity { get; set; } = 1;
    public float Scale { get; set; } = 1;

    private Rectangle _srcLeftSegmentRect;
    private Rectangle _dstLeftSegmentRect;

    private Rectangle _srcRightSegmentRect;
    private Rectangle _dstRightSegmentRect;

    private Rectangle _srcMiddleSegmentRect;
    private Rectangle _dstMiddleSegmentRect;
        
    private Rectangle _srcLeftGapRect;
    private Rectangle _dstLeftGapRect;
        
    private Rectangle _srcRightGapRect;
    private Rectangle _dstRightGapRect;

    public void RecalculateSourceRects()
    {
        if (Texture == null)
        {
            _srcLeftSegmentRect = _srcMiddleSegmentRect = _srcRightSegmentRect = _srcLeftGapRect = _srcRightGapRect = new Rectangle();

            return;
        }
        
        int leftSegmentSize = (int)(Texture.Width * LeftSegmentSize);
        int middleSegmentSize = (int)(Texture.Width * MiddleSegmentSize);
        int rightSegmentSize = (int)(Texture.Width * RightSegmentSize);

        _srcLeftSegmentRect = new Rectangle(0, 0, leftSegmentSize, Texture.Height);
        _srcMiddleSegmentRect = new Rectangle((Texture.Width - middleSegmentSize) / 2, 0, middleSegmentSize, Texture.Height);
        _srcRightSegmentRect = new Rectangle(Texture.Width - rightSegmentSize, 0, rightSegmentSize, Texture.Height);

        switch (GapRenderMethod)
        {
            case GapRenderMethodType.Stretch:
                _srcLeftGapRect = new Rectangle(_srcLeftSegmentRect.Right, 0, _srcMiddleSegmentRect.Left - _srcLeftSegmentRect.Right, Texture.Height);
                _srcRightGapRect = new Rectangle(_srcMiddleSegmentRect.Right, 0, _srcRightSegmentRect.Left - _srcMiddleSegmentRect.Right, Texture.Height);
                break;
            case GapRenderMethodType.FillFromCaps:
            {
                int leftGapWidth = Math.Min((int)((_dstMiddleSegmentRect.Left - _dstLeftSegmentRect.Right) / Scale), _srcMiddleSegmentRect.Left - _srcLeftSegmentRect.Right);
                int rightGapWidth = Math.Min((int)((_dstRightSegmentRect.Left - _dstMiddleSegmentRect.Right) / Scale), _srcRightSegmentRect.Left - _srcMiddleSegmentRect.Right);
                _srcLeftGapRect = new Rectangle(_srcLeftSegmentRect.Right, 0, leftGapWidth, Texture.Height);
                _srcRightGapRect = new Rectangle(_srcRightSegmentRect.Left - rightGapWidth, 0, rightGapWidth, Texture.Height);
            }
                break;
            case GapRenderMethodType.FillFromCenter:
            {
                int leftGapWidth = Math.Min((int)((_dstMiddleSegmentRect.Left - _dstLeftSegmentRect.Right) / Scale), _srcMiddleSegmentRect.Left - _srcLeftSegmentRect.Right);
                int rightGapWidth = Math.Min((int)((_dstRightSegmentRect.Left - _dstMiddleSegmentRect.Right) / Scale), _srcRightSegmentRect.Left - _srcMiddleSegmentRect.Right);
                _srcLeftGapRect = new Rectangle(_srcMiddleSegmentRect.Left - leftGapWidth, 0, leftGapWidth, Texture.Height);
                _srcRightGapRect = new Rectangle(_srcRightSegmentRect.Right, 0, rightGapWidth, Texture.Height);
            }
                break;
        }
    }
    
    public override void Recalculate()
    {
        base.Recalculate();

        var dimensions = GetDimensions();
            
        int leftSegmentSize = (int)(Texture.Width * LeftSegmentSize * Scale);
        int middleSegmentSize = (int)(Texture.Width * MiddleSegmentSize * Scale);
        int rightSegmentSize = (int)(Texture.Width * RightSegmentSize * Scale);

        _dstLeftSegmentRect = new Rectangle((int)dimensions.X, (int)dimensions.Y, leftSegmentSize, (int)dimensions.Height);
        _dstMiddleSegmentRect = new Rectangle((int)(dimensions.X + (dimensions.Width - middleSegmentSize) / 2), (int)dimensions.Y, middleSegmentSize, (int)dimensions.Height);
        _dstRightSegmentRect = new Rectangle((int)(dimensions.X + dimensions.Width - rightSegmentSize), (int)dimensions.Y, rightSegmentSize, (int)dimensions.Height);

        float oldLeftGapWidth = _dstLeftGapRect.Width;
        float oldRightGapWidth = _dstRightGapRect.Width;
        
        _dstLeftGapRect = new Rectangle(_dstLeftSegmentRect.Right, (int)dimensions.Y, _dstMiddleSegmentRect.Left - _dstLeftSegmentRect.Right, (int)dimensions.Height);
        _dstRightGapRect = new Rectangle(_dstMiddleSegmentRect.Right, (int)dimensions.Y, _dstRightSegmentRect.Left - _dstMiddleSegmentRect.Right, (int)dimensions.Height);

        if (GapRenderMethod is GapRenderMethodType.FillFromCaps or GapRenderMethodType.FillFromCenter && (oldLeftGapWidth != _dstLeftGapRect.Width || oldRightGapWidth != _dstRightGapRect.Width))
        {
            RecalculateSourceRects();
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        // var dimensions = GetDimensions();
        // spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height), Color.Red * 0.5f);
            
        spriteBatch.Draw(Texture, _dstMiddleSegmentRect, _srcMiddleSegmentRect, Color.White * Opacity);

        spriteBatch.Draw(Texture, _dstLeftSegmentRect, _srcLeftSegmentRect, Color.White * Opacity);
        spriteBatch.Draw(Texture, _dstRightSegmentRect, _srcRightSegmentRect, Color.White * Opacity);

        if (_dstLeftSegmentRect.Right < _dstMiddleSegmentRect.Left)
        {
            spriteBatch.Draw(Texture, _dstLeftGapRect, _srcLeftGapRect, Color.White * Opacity);
        }

        if (_dstMiddleSegmentRect.Right < _dstRightSegmentRect.Left)
        {
            spriteBatch.Draw(Texture, _dstRightGapRect, _srcRightGapRect, Color.White * Opacity);
        }
    }
}