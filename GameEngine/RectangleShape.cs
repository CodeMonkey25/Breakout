using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMain.GameEngine;

public class RectangleShape
{
    private Rectangle _bounds;

    public RectangleShape(GraphicsDevice graphicsDevice, int size, Color color)
        : this(graphicsDevice, size, size, color)
    {
    }

    public RectangleShape(GraphicsDevice graphicsDevice, int width, int height, Color color)
    {
        _bounds = new Rectangle(Point.Zero, new Point(width, height));
        Texture = new Texture2D(graphicsDevice, 1, 1);
        Texture.SetData(new[] { color });
    }

    public Texture2D Texture { get; }

    public Rectangle Bounds => _bounds;

    public int X => _bounds.X;
    public int Y => _bounds.Y;
    public int Width => _bounds.Width;
    public int Height => _bounds.Height;

    public void UpdateX(int x)
    {
        _bounds.X = x;
    }

    public void UpdateY(int y)
    {
        _bounds.Y = y;
    }

    public void UpdatePosition(int x, int y)
    {
        UpdateX(x);
        UpdateY(y);
    }
}