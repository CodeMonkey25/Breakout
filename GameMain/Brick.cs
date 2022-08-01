using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameMain;

public class Brick
{
    public const int RequestedWidth = 50;
    public const int RequestedHeight = 20;
    public const int Cushion = 10;

    private RectangleShape Sprite { get; }

    public Brick(GraphicsDevice graphicsDevice, int x, int y)
    {
        Sprite = new RectangleShape(graphicsDevice, RequestedWidth, RequestedHeight, Color.Red);
        Sprite.UpdatePosition(x, y);
    }

    public void Update(GameTime gameTime)
    {
        
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(Sprite.Texture, Sprite.Rectangle, Color.White);
        spriteBatch.End();
    }
}