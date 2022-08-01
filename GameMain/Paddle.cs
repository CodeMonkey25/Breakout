using System;
using GameMain.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameMain;

public class Paddle
{
    private const int width = 100;
    private const int height = 20;
    private const float speed = 0.3f;
    
    private readonly int maxX;
    private readonly RectangleShape sprite;
    private Vector2 position;

    public int X => (int)position.X;
    public int Y => (int)position.Y;
    public int Width => sprite.Width;
    public int Height => sprite.Height;
    
    public Paddle(GraphicsDevice graphicsDevice)
    {
        sprite = new RectangleShape(graphicsDevice, width, height, Color.Black);
        maxX = graphicsDevice.Viewport.Width - sprite.Width;
        int maxY = graphicsDevice.Viewport.Height - sprite.Height;
        position = new Vector2(
            maxX / 2,
            maxY - 25
        );
    }

    public void Update(GameTime gameTime, MouseState mouseState)
    {
        int startingX = (int)position.X + width / 2;
        int targetX = mouseState.X;  
        
        int multiplier = 0;
        if (targetX < startingX)
        {
            multiplier = -1;
        }
        else if (targetX > startingX)
        {
            multiplier = 1;
        }

        if (multiplier == 0)
        {
            return;
        }

        int delta = (int)(speed * gameTime.ElapsedGameTime.TotalMilliseconds);
        delta = Math.Min(delta, Math.Abs(startingX - targetX));
        delta *= multiplier;
        if (delta == 0)
        {
            return;
        }
        
        int newX = (int)position.X + delta;
        newX = Math.Min(newX, maxX);
        newX = Math.Max(newX, 0);
        position.X = Math.Min(newX, maxX);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(sprite.GetTexture(), position, Color.White);
        spriteBatch.End();
    }
}