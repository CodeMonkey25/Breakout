using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameMain;

public class Brick
{
    public const int Width = 50;
    public const int Height = 20;
    public const int Cushion = 10;

    private readonly RectangleF _rectangle;
    public RectangleF Rectangle => _rectangle;

    public Brick(GraphicsDevice graphicsDevice, int x, int y)
    {
        _rectangle = new RectangleF(x, y, Width, Height);
    }

    public void Update(GameTime gameTime)
    {
        
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.FillRectangle(_rectangle, Color.Red);
        spriteBatch.End();
    }
}