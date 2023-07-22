using Microsoft.Xna.Framework;

namespace BTitles;

/**
 * A class that contains important info to perform animation
 */
public class AnimationConfig
{
    // Total animation duration in seconds
    public float Duration;
        
    // In transition duration in seconds
    public float InTime;
        
    // Out transition duration in seconds
    public float OutTime;
}

/**
 * A class that contains basic title animations
 */
public static class TitleAnimations
{
    public static void AnimateShowFade(float time, AnimationConfig config, BiomeTitle title, BiomeTitle subtitle)
    {
        bool infiniteStay = config.Duration <= 0;
        
        if (time < config.InTime)
        {
            float alpha = time / config.InTime;
            
            title.Opacity = alpha;

            if (subtitle != null)
            {
                subtitle.Opacity = alpha;
            }
        }
        else if (time < config.Duration - config.OutTime || infiniteStay)
        {
            //float alpha = (time - config.InTime) / (config.Duration - config.InTime - config.OutTime);

            title.Opacity = 1;

            if (subtitle != null)
            {
                subtitle.Opacity = 1;
            }
        }
        else
        {
            float alpha = (time - (config.Duration - config.InTime)) / config.OutTime;
            
            title.Opacity = 1 - alpha;

            if (subtitle != null)
            {
                subtitle.Opacity = 1 - alpha;
            }
        }
    }

    public static void AnimateShowFadeSwipe(float time, AnimationConfig config, BiomeTitle title, BiomeTitle subtitle)
    {
        bool infiniteStay = config.Duration <= 0;
        
        float P0 = infiniteStay ? -100 : -125;
        float P1 = infiniteStay ? 0 : -25;
        float P2 = 25;
        float P3 = 125;
        
        if (time < config.InTime)
        {
            float alpha = time / config.InTime;
            
            title.Opacity = alpha;
            title.MarginLeft = MathHelper.Lerp(P0, P1, alpha);
            title.Recalculate();

            if (subtitle != null)
            {
                subtitle.Opacity = alpha;
                subtitle.MarginRight = MathHelper.Lerp(P0, P1, alpha);
                subtitle.Recalculate();
            }
        }
        else if (time < config.Duration - config.OutTime || infiniteStay)
        {
            float alpha = infiniteStay ? 0 : (time - config.InTime) / (config.Duration - config.InTime - config.OutTime);

            title.Opacity = 1;
            title.MarginLeft = MathHelper.Lerp(P1, P2, alpha);
            title.Recalculate();

            if (subtitle != null)
            {
                subtitle.Opacity = 1;
                subtitle.MarginRight = MathHelper.Lerp(P1, P2, alpha);
                subtitle.Recalculate();
            }
        }
        else
        {
            float alpha = (time - (config.Duration - config.InTime)) / config.OutTime;
            
            title.Opacity = 1 - alpha;
            title.MarginLeft = MathHelper.Lerp(P2, P3, alpha);
            title.Recalculate();

            if (subtitle != null)
            {
                subtitle.Opacity = 1 - alpha;
                subtitle.MarginRight = MathHelper.Lerp(P2, P3, alpha);
                subtitle.Recalculate();
            }
        }
    }
}
