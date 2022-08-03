using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameMain;

public class Brick
{
    public const int Width = 50;
    public const int Height = 20;
    public const int Cushion = 10;

    public RectangleF Rectangle { get; }

    public Brick(GraphicsDevice graphicsDevice, int x, int y)
    {
        Rectangle = new RectangleF(x, y, Width, Height);
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.FillRectangle(Rectangle, Color.Red);
        spriteBatch.End();
    }
}