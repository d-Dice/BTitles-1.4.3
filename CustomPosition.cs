using Microsoft.Xna.Framework;
public class CustomPosition
{
    public float X { get; set; }
    public float Y { get; set; }

    public CustomPosition() { }

    public CustomPosition(float x, float y)
    {
        X = x;
        Y = y;
    }
    
    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }
}