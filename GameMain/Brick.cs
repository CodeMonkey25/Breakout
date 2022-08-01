using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameMain;

public class Brick
{
    public const int RequestedWidth = 50;
    public const int RequestedHeight = 20;
    public const int Cushion = 10;

    private readonly Vector2 _location;
    private readonly Size2 _size;

    public Brick(GraphicsDevice graphicsDevice, int x, int y)
    {
        _location = new Vector2(x, y);
        _size = new Size2(RequestedWidth, RequestedHeight);
    }

    public void Update(GameTime gameTime)
    {
        
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.FillRectangle(_location, _size, Color.Red);
        spriteBatch.End();
    }
}