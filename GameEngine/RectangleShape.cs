using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMain.GameEngine;

public class RectangleShape
{
    private readonly Texture2D _texture;
    public Texture2D Texture => _texture;
    
    private Rectangle _rectangle;
    public Rectangle Rectangle => _rectangle;
    
    public int X => _rectangle.X;
    public int Y => _rectangle.Y;
    public int Width => _rectangle.Width; 
    public int Height => _rectangle.Height;

    public RectangleShape(GraphicsDevice graphicsDevice, int size, Color color)
    : this(graphicsDevice, size, size, color)
    {
    }
    
    public RectangleShape(GraphicsDevice graphicsDevice, int width, int height, Color color)
    {
        _rectangle = new Rectangle(Point.Zero, new Point(width, height));
        _texture = new Texture2D(graphicsDevice, 1, 1);
        _texture.SetData(new[] { color });
    }

    public void UpdateX(int x)
    {
        _rectangle.X = x;
    }

    public void UpdateY(int y)
    {
        _rectangle.Y = y;
    }

    public void UpdatePosition(int x, int y)
    {
        UpdateX(x);
        UpdateY(y);
    }
}